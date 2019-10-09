using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using WebMVC.Infrastructure;
using WebMVC.Services;
using System.Net.Http;
using Polly;
using Polly.Extensions.Http;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using IdentityModel.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Razor;
using WebMVC.Attributes;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using AspNetCore.Proxy;
using WebMVC.Models;

namespace WebMVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddCustomMvcWithLocalization(Configuration)
                .AddHttpClientServices(Configuration)
                .AddCustomAuthentication(Configuration)
                .AddProxies();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
                app.UseHttpsRedirection();
            }
       
            app.UseAuthentication();

            app.UseStaticFiles();

            app.UseCustomLocalization(Configuration);

            app.UseCookiePolicy();

            app.UseMvcWithDefaultRoute();
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
                options.Filters.Add(new AuthorizeFilter());

            })
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization()
                //.AddDataAnnotationsLocalization(o =>
                //{
                //    o.DataAnnotationLocalizerProvider = (type, factory) =>
                //    {
                //        return factory.Create(typeof(SharedResource));
                //    };
                //})
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("sr-Latn-RS")
                };

                // Setting only DefaultRequestCulture doesn't work
                // https://forums.asp.net/t/2151316.aspx?Why+DefaultRequestCulture+does+not+work+in+ASP+NET+Core
                var defaultCulture = configuration["DefaultCulture"];

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

        public static IServiceCollection AddHttpClientServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //register delegating handlers
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
            services.AddTransient<CorrelationIdDelegatingHandler>();
            
            //set 5 min as the lifetime for each HttpMessageHandler int the pool
            services.AddHttpClient("extendedhandlerlifetime").SetHandlerLifetime(TimeSpan.FromMinutes(5));

            //add http client services
            services.AddHttpClient<IUserService, UserService>()
                   .SetHandlerLifetime(TimeSpan.FromMinutes(5))  //Sample. Default lifetime is 2 minutes
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddHttpMessageHandler<CorrelationIdDelegatingHandler>()
                   .AddPolicyHandler(GetRetryPolicy())
                   .AddPolicyHandler(GetCircuitBreakerPolicy());

            services.AddHttpClient<IContactService, ContactService>()
                   .SetHandlerLifetime(TimeSpan.FromMinutes(5))  //Sample. Default lifetime is 2 minutes
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddHttpMessageHandler<CorrelationIdDelegatingHandler>()
                   .AddPolicyHandler(GetRetryPolicy())
                   .AddPolicyHandler(GetCircuitBreakerPolicy());

            services.AddHttpClient<IFormService, FormService>()
                  .SetHandlerLifetime(TimeSpan.FromMinutes(5))  //Sample. Default lifetime is 2 minutes
                  .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddHttpMessageHandler<CorrelationIdDelegatingHandler>()
                  .AddPolicyHandler(GetRetryPolicy())
                  .AddPolicyHandler(GetCircuitBreakerPolicy());

            services.AddHttpClient<ILogService, LogService>()
                  .SetHandlerLifetime(TimeSpan.FromMinutes(5))  //Sample. Default lifetime is 2 minutes
                  .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddHttpMessageHandler<CorrelationIdDelegatingHandler>()
                  .AddPolicyHandler(GetRetryPolicy())
                  .AddPolicyHandler(GetCircuitBreakerPolicy());

            return services;
        }

        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); //custom

            var identityUrl = configuration.GetValue<string>("IdentityUrl");
            var callBackUrl = configuration.GetValue<string>("CallBackUrl");
            var clientId = configuration.GetValue<string>("ClientId");
            var clientSecret = configuration.GetValue<string>("ClientSecret");


            services
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddCookie(setup =>
                {
                    setup.SlidingExpiration = true;
                    setup.ExpireTimeSpan = TimeSpan.FromMinutes(double.Parse(configuration["CookieLifetime"]));
                })
                // AddAutomaticTokenManagement is used to automaticlly renew access_token with renew_token
                .AddAutomaticTokenManagement(opt =>
                {
                    opt.RefreshBeforeExpiration = TimeSpan.FromMinutes(double.Parse(configuration["RefreshBeforeExpiration"]));
                })
                .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
                {
                    //options.Events.OnRedirectToIdentityProvider = context =>
                    //{
                    //    context.ProtocolMessage.Prompt = "login";
                    //    return Task.CompletedTask;
                    //};

                    options.Authority = identityUrl.ToString();
                    options.SignedOutRedirectUri = callBackUrl.ToString();
                    options.RequireHttpsMetadata = false; // PRODUCTION - should stay true

                    options.ClientId = clientId;
                    options.ClientSecret = clientSecret;
                    options.ResponseType = "code id_token";

                    options.SaveTokens = true;
                    options.GetClaimsFromUserInfoEndpoint = true;

                    options.Scope.Clear();
                    options.Scope.Add("ApiGateway");
                    options.Scope.Add("offline_access");
                    options.Scope.Add("profile");
                    options.Scope.Add("openid");
                    options.Scope.Add("SignalR");
                    options.Scope.Add("Contact");
                    options.Scope.Add("Identity");
                    options.Scope.Add("Form");
                    options.Scope.Add("Session");
                    options.Scope.Add("Logs");

                    options.ClaimActions.MapJsonKey("role", "role", "role"); // mapping role claims
                    options.ClaimActions.MapJsonKey("permission", "permission", "permission"); // mapping permission claims

                    options.TokenValidationParameters = new TokenValidationParameters // role as resource
                    {
                        NameClaimType = "name",
                        RoleClaimType = "role"
                    };
                });

            services.AddSingleton<IIdentityParser<ApplicationUser>,IdentityParser>();

            return services;
        }

        public static IApplicationBuilder UseCustomLocalization(this IApplicationBuilder app, IConfiguration configuration)
        {
            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/localization?view=aspnetcore-2.2
            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();

            app.UseRequestLocalization(locOptions.Value);

            return app;
        }

        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
              .HandleTransientHttpError()
              .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
              .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

        }

        static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
        }
    }
}