using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Audit.Core;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using BuildingBlocks.EventBusProjects.EventBus;
using BuildingBlocks.EventBusProjects.EventBus.Abstractions;
using BuildingBlocksEventBusProjects.EventBusRabbitMQ;
using Contact.API.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RabbitMQ.Client;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;

namespace Contact.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCustomMVC(Configuration)
                .AddCustomDbContext(Configuration)
                .AddCustomAuthentication(Configuration)
                .AddRabbitMQConnection(Configuration)
                .RegisterEventBus(Configuration)
                .AddCustomSwagger(Configuration);
            //.AddProxy();

            ConfigureAuditLogging();

            //configure autofac
            var container = new ContainerBuilder();
            container.Populate(services);

            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IHttpContextAccessor ctxAccessor)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // app.UseHttpsRedirection();  // Not needed if using gateway, which will handle https
                // app.UseHsts(); // Not needed if using gateway, which will handle https
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contact API V1");
            });

            AuditLoggingEventSetup(ctxAccessor);

            app.UseAuthentication();

            app.UseMvc();
        }

        private void ConfigureAuditLogging()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder<AuditLogDbContext>().UseSqlServer(Configuration.GetConnectionString("AuditDb"), sqlServerOptionsAction: sqlOptions =>
            {
                //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
            });

            Audit.Core.Configuration.Setup()
                .UseEntityFramework(ef => ef
                    .UseDbContext<AuditLogDbContext>(optionsBuilder.Options)
                    .AuditTypeMapper(t => typeof(AuditLog))
                    .AuditEntityAction<AuditLog>((ev, entry, entity) =>
                    {
                        entity.AuditData = entry.Action == "Update" ? JsonConvert.SerializeObject(entry.Changes) : JsonConvert.SerializeObject(entry.ColumnValues);
                        entity.EntityType = entry.EntityType.Name;
                        entity.AuditDate = DateTime.Now;
                        entity.Title = ev.CustomFields.TryGetValue("Title", out var title) ? title.ToString() : null;
                        entity.AuditAction = entry.Action;
                        entity.TablePk = entry.PrimaryKey.First().Value.ToString();
                        entity.AuditUsername = ev.Environment.UserName != "Unauthenticated" ? ev.Environment.UserName : ev.CustomFields.TryGetValue("AuditUsername", out var username) ? username.ToString() : "Unauthenticated";
                        entity.TableName = entry.Table;
                        entity.CorrelationId = ev.CustomFields["CorrelationId"].ToString();
                    })
                    .IgnoreMatchedProperties(true));
        }

        private void AuditLoggingEventSetup(IHttpContextAccessor ctxAccessor)
        {
            Audit.Core.Configuration.AddCustomAction(ActionType.OnScopeCreated, scope =>
            {
                if (ctxAccessor.HttpContext == null) // Context is null in case of event handling
                {
                    scope.Event.Environment.UserName = "Unauthenticated";
                    scope.SetCustomField("CorrelationId", "Uncorrelated");
                }
                else
                {
                    scope.Event.Environment.UserName = ctxAccessor.HttpContext.User.Identity.IsAuthenticated ? ctxAccessor.HttpContext.User.Identity.Name : "Unauthenticated";
                    // Get or set correlationId header
                    string correlationId = "Uncorrelated";
                    if (ctxAccessor.HttpContext != null)
                    {
                        if (string.IsNullOrWhiteSpace(ctxAccessor.HttpContext.Request.Headers["X-Correlation-ID"]))
                            ctxAccessor.HttpContext.Request.Headers["X-Correlation-ID"] = Guid.NewGuid().ToString();

                        correlationId = ctxAccessor.HttpContext.Request.Headers["X-Correlation-ID"];
                    }

                    scope.SetCustomField("CorrelationId", correlationId);
                }
            });
        }
    }


    static class ServisCollectionExtension
    {
        public static IServiceCollection AddCustomMVC(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(
                    options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                )
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            return services;
        }

        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ContactContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("ContactDb"),
                                     sqlServerOptionsAction: sqlOptions =>
                                     {
                                         sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                                         //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                                         sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                                     });
            });

            return services;
        }

        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            // prevent from mapping "sub" claim to nameidentifier.
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

            //Used to know where is IdentityServer4 located to go validate the token
            //and set the name of this API as audience, which is used at identityserver
            //as scope to which client can have access to
            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = configuration["IdentityUrl"];
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "Contact";
                    options.ApiSecret = "secret";

                    options.EnableCaching = true;
                    options.CacheDuration = TimeSpan.FromMinutes(10);
                });

            return services;
        }

        public static IServiceCollection AddRabbitMQConnection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

                var factory = new ConnectionFactory()
                {
                    HostName = "localhost",
                    DispatchConsumersAsync = true,
                    UserName = configuration["RabbitMQUsername"],
                    Password = configuration["RabbitMQPassword"]
                };

                var retryCount = 5;
                if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
                {
                    retryCount = int.Parse(configuration["EventBusRetryCount"]);
                }

                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            });

            return services;
        }

        public static IServiceCollection RegisterEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            var subscriptionClientName = configuration["SubscriptionClientName"];

            services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
            {
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                var retryCount = 5;
                if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
                {
                    retryCount = int.Parse(configuration["EventBusRetryCount"]);
                }

                return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
            });

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            return services;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
              {
                  c.SwaggerDoc("v1", new Info { Title = "Contact API", Version = "v1" });

                  //var filePath = Path.Combine(System.AppContext.BaseDirectory, "Contact.API.xml");
                  var filePath = configuration["SwaggerXMLDoc"];

                  c.IncludeXmlComments(filePath);

                  // Adds "(Auth)" to the summary so that you can see which endpoints have Authorization
                  c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>(); 

                  // add Security information to each operation for OAuth2
                  c.OperationFilter<SecurityRequirementsOperationFilter>();
                  // or use the generic method, e.g. c.OperationFilter<SecurityRequirementsOperationFilter<MyCustomAttribute>>();

                  // if you're using the SecurityRequirementsOperationFilter, you also need to tell Swashbuckle you're using OAuth2
                  c.AddSecurityDefinition("oauth2", new ApiKeyScheme
                  {
                      Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                      In = "header",
                      Name = "Authorization",
                      Type = "apiKey"
                  });
              });

            return services;
        }
    }
}
