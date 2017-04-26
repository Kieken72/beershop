using System.Reflection;
using Catalog.API.Infrastructure.Filters;
using Catalog.API.Infrastructure.Repositories;
using Catalog.API.IntegrationEvents;
using EventBus.Abstractions;
using EventBusRabbitMQ;
using IntegrationEventLogMemory.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.HealthChecks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Swashbuckle.Swagger.Model;

namespace Catalog.API
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile($"settings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"settings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets(typeof(Startup).GetTypeInfo().Assembly);
            }

            builder.AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            }).AddControllersAsServices();

            services.AddHealthChecks(checks =>
            {
                checks.AddSqlCheck("CatalogDb", Configuration["ConnectionString"]);
            });

            services.Configure<CatalogSettings>(Configuration);

            // Add framework services.
            services.AddSwaggerGen();
            services.ConfigureSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SingleApiVersion(new Info
                {
                    Title = "Catalog HTTP API",
                    Version = "v1",
                    Description = "The Catalog Microservice HTTP API. This is a Data-Driven/CRUD microservice sample",
                    TermsOfService = "Terms Of Service"
                });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddTransient<ICatalogRepository, CatalogRepository>();
            services.AddTransient<IIntegrationEventLogService, IntegrationEventLogService>();
            services.AddTransient<ICatalogIntegrationEventService, CatalogIntegrationEventService>();

            services.AddSingleton<IRabbitMQPersisterConnection>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<CatalogSettings>>().Value;
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersisterConnection>>();
                var factory = new ConnectionFactory
                {
                    HostName = settings.EventBusConnection
                };

                return new DefaultRabbitMQPersisterConnection(factory, logger);
            });

            services.AddSingleton<IEventBus, EventBusRabbitMQ.EventBusRabbitMQ>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //Configure logs
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCors("CorsPolicy");

            app.UseMvcWithDefaultRoute();

            app.UseSwagger()
              .UseSwaggerUi();
        }
    }
}
