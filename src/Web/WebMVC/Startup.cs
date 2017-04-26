using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.HealthChecks;
using Resilience.Http;
using WebMVC.Services;
using WebMVC.Infrastructure;
using WebMVC.ViewModels;

namespace WebMVC
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                      .SetBasePath(env.ContentRootPath)
                      .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)  // Settings for the application
                      .AddEnvironmentVariables();                                              // override settings with environment variables set in compose.   


            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDataProtection(opts =>
            {
                opts.ApplicationDiscriminator = "beershop.webmvc";
            });

            services.AddMvc();
            services.Configure<AppSettings>(Configuration);

            services.AddHealthChecks(checks =>
            {
                checks.AddUrlCheck(Configuration["CatalogUrl"]);
            });

            // Add application services.
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();            
            services.AddTransient<ICatalogService, CatalogService>(); 
            services.AddTransient<IIdentityParser<ApplicationUser>, IdentityParser>();

            if (Configuration.GetValue<string>("UseResilientHttp") == bool.TrueString)
            {
                services.AddTransient<IResilientHttpClientFactory, ResilientHttpClientFactory>();
                services.AddTransient<IHttpClient, ResilientHttpClient>(sp => sp.GetService<IResilientHttpClientFactory>().CreateResilientHttpClient());
            }
            else
            {
                services.AddTransient<IHttpClient, StandardHttpClient>();
            }
        }        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Catalog/Error");
            }

            app.UseStaticFiles();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "Cookies",
                AutomaticAuthenticate = true,
            });

            var identityUrl = Configuration.GetValue<string>("IdentityUrl");
            var callBackUrl = Configuration.GetValue<string>("CallBackUrl");
            var log = loggerFactory.CreateLogger("identity");

            var oidcOptions = new OpenIdConnectOptions
            {
                AuthenticationScheme = "oidc",
                SignInScheme = "Cookies",
                Authority = identityUrl.ToString(),
                PostLogoutRedirectUri = callBackUrl.ToString(), 
                ClientId = "mvc",
                ClientSecret = "secret",
                ResponseType = "code id_token", 
                SaveTokens = true,
                GetClaimsFromUserInfoEndpoint = true,
                RequireHttpsMetadata = false,
                Scope = { "openid", "profile", "orders", "basket" }
            };

            //Wait untill identity service is ready on compose. 
            app.UseOpenIdConnectAuthentication(oidcOptions);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Catalog}/{action=Index}/{id?}");
            });
        }
    }
}
