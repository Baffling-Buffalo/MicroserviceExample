// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Services;
using Identity.API.Data;
using Identity.API.Services;
using Identity.API.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Globalization;
using System.Reflection;
using System.Linq;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.Filters;
using Identity.API.Filters;
using Newtonsoft.Json.Serialization;
using System.IdentityModel.Tokens.Jwt;
using BuildingBlocksEventBusProjects.EventBusRabbitMQ;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using BuildingBlocks.EventBusProjects.EventBus.Abstractions;
using Autofac;
using BuildingBlocks.EventBusProjects.EventBus;
using Identity.API.Infrastructure;
using Polly;
using Polly.Extensions.Http;
using System.Net.Http;
using Identity.API.IntegrationEvents.Events;
using Identity.API.IntegrationEvents.Handlers;
using Autofac.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Audit.SqlServer.Providers;
using Audit.SqlServer;
using System.Collections.Generic;
using Audit.Core;

namespace Identity.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddCustomMvcWithLocalization(Configuration)
                .AddCustomDbContext(Configuration)
                .AddIdentityServer(Configuration, Environment)
                .AddCustomAuthentication(Configuration)
                .AddHttpClientServices(Configuration)
                .AddRabbitMQConnection(Configuration)
                .RegisterEventBus(Configuration)
                .AddCustomSwagger(Configuration)
                .AddAuditLogging(Configuration);

            //configure autofac
            var container = new ContainerBuilder();
            container.Populate(services);

            return new AutofacServiceProvider(container.Build());
        }

        public void Configure(IApplicationBuilder app, IHttpContextAccessor ctxAccessor)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                //app.UseHttpsRedirection(); 
            }

            app.UseAuditWithEventBus();
            //Audit.Core.Configuration.DataProvider = new AuditEventBusDataProvider(app.ApplicationServices.GetRequiredService<IEventBus>(), Configuration["AppName"]);
            //AuditLoggingEventSetup(ctxAccessor);

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger()

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            .UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity API V1");
            })
            .UseStaticFiles()
            .UseIdentityServer()
            .UseCustomLocalization(Configuration)
            .UseMvcWithDefaultRoute()
            .ConfigureEventBus();
        }

        private void AuditLoggingEventSetup(IHttpContextAccessor ctxAccessor)
        {
            Audit.Core.Configuration.AddCustomAction(ActionType.OnScopeCreated, scope =>
            {
                if (ctxAccessor.HttpContext == null) // Context will be null in case of event handling
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

    static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomMvcWithLocalization(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<AppSettings>(configuration);

            services.AddLocalization(options =>
            {
                options.ResourcesPath = "Resources";
            });

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ModelValidationFilter));
            })
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options => {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver(); // So as to api returs json with Properties formatted with first char to upper case instead of camelcase
                });

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("sr-Latn-RS")
                };

                // Setting only DefaultRequestCulture doesn't work
                // https://forums.asp.net/t/2151316.aspx?Why+DefaultRequestCulture+does+not+work+in+ASP+NET+Core
                var defaultCulture = configuration["defaultCulture"];

                var cookieProvider = options.RequestCultureProviders
                    .OfType<CookieRequestCultureProvider>()
                    .First();
                var urlProvider = options.RequestCultureProviders
                    .OfType<QueryStringRequestCultureProvider>()
                    .First();

                cookieProvider.Options.DefaultRequestCulture = new RequestCulture(defaultCulture);
                urlProvider.Options.DefaultRequestCulture = new RequestCulture(defaultCulture);

                options.RequestCultureProviders.Clear();
                options.RequestCultureProviders.Add(cookieProvider);
                options.RequestCultureProviders.Add(urlProvider);

                options.DefaultRequestCulture = new RequestCulture(culture: defaultCulture, uiCulture: defaultCulture);

                options.SupportedCultures = supportedCultures;

                options.SupportedUICultures = supportedCultures;

            });

            return services;
        }

        // Adds all Http client services (like Service-Agents) using resilient Http requests based on HttpClient factory and Polly's policies 
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("IdentityDb"), sqlServerOptionsAction: sqlOptions =>
                {
                    //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                }));
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();


            return services;
        }

        public static IServiceCollection AddIdentityServer(this IServiceCollection services, IConfiguration configuration, IHostingEnvironment environment)
        {
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
                .AddAspNetIdentity<ApplicationUser>()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b =>
                        b.UseSqlServer(configuration.GetConnectionString("IdentityDb"),
                            sql => sql.MigrationsAssembly(nameof(Identity.API)));
                })
                // this adds the operational data from DB (codes, tokens, consents)
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b =>
                        b.UseSqlServer(configuration.GetConnectionString("IdentityDb"),
                            sql => sql.MigrationsAssembly(nameof(Identity.API)));

                    // this enables automatic token cleanup. this is optional.
                    options.EnableTokenCleanup = true;
                });

            services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(double.Parse(configuration["CookieLifetime"]));
                options.SlidingExpiration = true;
            });

            services.AddTransient<IProfileService, ProfileService>();

            if (environment.IsDevelopment())
            {
                builder.AddDeveloperSigningCredential();
            }
            else
            {
                // TODO: uncomment for production, call builder.addsigningcredentials and add certificate
                //throw new Exception("need to configure key material");
                builder.AddDeveloperSigningCredential();
            }

            return services;
        }

        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication()
                // Api service token authentication scheme
                .AddIdentityServerAuthentication("Bearer",options =>
                {
                    options.Authority = configuration["AppUrl"];
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "Identity";
                    options.ApiSecret = "secret";
                    
                    options.EnableCaching = true;
                    options.CacheDuration = TimeSpan.FromMinutes(10);
                })
                // Add google sign in support
                .AddGoogle(options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    // register your IdentityServer with Google at https://console.developers.google.com
                    // enable the Google+ API
                    // set the redirect URI to http://localhost:5000/signin-google
                    options.ClientId = "1061140992155-g0a7ptjru5bmeiigcjl335rkt2nr0e36.apps.googleusercontent.com";
                    options.ClientSecret = "0XMi7tUm4fs2OkkcCEISug0b";
                });

            // Active directory signin support
            services.Configure<IISOptions>(iis =>
            {
                iis.AuthenticationDisplayName = "Windows";
                iis.AutomaticAuthentication = false;
            });

            return services;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Identity API", Version = "v1" });

                //var filePath = Path.Combine(System.AppContext.BaseDirectory, "Identity.API.xml");
                var filePath = configuration["SwaggerXMLDoc"];

                c.IncludeXmlComments(filePath);

                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>(); // Adds "(Auth)" to the summary so that you can see which endpoints have Authorization
                                                                              // or use the generic method, e.g. c.OperationFilter<AppendAuthorizeToSummaryOperationFilter<MyCustomAttribute>>();

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

        public static IApplicationBuilder UseCustomLocalization(this IApplicationBuilder app, IConfiguration configuration)
        {
            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/localization?view=aspnetcore-2.2
            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);

            return app;
        }

        public static IServiceCollection AddHttpClientServices(this IServiceCollection services, IConfiguration configuration)
        {
            //register delegating handlers
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
            //register delegating handlers
            services.AddTransient<CorrelationIdDelegatingHandler>();
           
            //set 5 min as the lifetime for each HttpMessageHandler int the pool
            services.AddHttpClient("extendedhandlerlifetime").SetHandlerLifetime(TimeSpan.FromMinutes(5));

            services.AddHttpClient<IContactService, ContactService>()
                   .SetHandlerLifetime(TimeSpan.FromMinutes(5))  //Sample. Default lifetime is 2 minutes
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddHttpMessageHandler<CorrelationIdDelegatingHandler>();

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
            services.AddTransient<ContactsDeletedIntegrationEventHandler>();

            return services;
        }

        public static IApplicationBuilder ConfigureEventBus(this IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<ContactsDeletedIntegrationEvent, ContactsDeletedIntegrationEventHandler>();

            return app;
        }

        public static IServiceCollection AddAuditLogging(this IServiceCollection services, IConfiguration configuration)
        {
            //Audit.Core.Configuration.DataProvider = new SqlDataProvider()
            //{
            //    ConnectionString = configuration.GetConnectionString("AuditDb"),
            //    Schema = "dbo",
            //    TableName = "AuditLogs",
            //    IdColumnName = "Id",
            //    CustomColumns = new List<CustomColumn>()
            //    {
            //        new CustomColumn("EntityType", ev => ev.CustomFields.TryGetValue("EntityType",out var entity) ? entity.ToString() : ""),
            //        new CustomColumn("AuditAction", ev => ev.CustomFields.TryGetValue("AuditAction",out var entity) ? entity.ToString() : ""),
            //        new CustomColumn("Title", ev => ev.CustomFields.TryGetValue("Title",out var title) ? title.ToString() : ""),
            //        new CustomColumn("AuditDate", ev => DateTime.Now),
            //        new CustomColumn("AuditUsername", ev => ev.Environment.UserName != "Unauthenticated" ? ev.Environment.UserName : ev.CustomFields.TryGetValue("AuditUsername",out var username) ? username.ToString() : "Unauthenticated"),
            //        new CustomColumn("CorrelationId", ev => ev.CustomFields["CorrelationId"].ToString()),
            //        new CustomColumn("TableName", ev => ev.CustomFields.TryGetValue("TableName",out var entity) ? entity.ToString() : ""),
            //        new CustomColumn("TablePK", ev => ev.CustomFields.TryGetValue("TablePK",out var entity) ? entity.ToString() : ""),
            //        new CustomColumn("AuditData", ev => ev.CustomFields.TryGetValue("AuditData",out var entity) ? entity.ToString() : ""),
            //    }
            //};

            services.AddSingleton<IAuditLogService, AuditLogService>();

            return services;
        }

        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
              .HandleTransientHttpError()
              .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
              .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

        }
    }
}