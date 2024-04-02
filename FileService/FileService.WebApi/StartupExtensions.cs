using Fileservice.DataStore.Minio.NotebookDataProvider;
using Fileservice.DataStore.Mongo;
using Fileservice.DataStore.Mongo.NotebookDataProvider;
using Fileservice.WebApi.Services.NotebookFacade;
using Fileservice.WebApi.Settings;
using Microsoft.OpenApi.Models;
using Minio;
using MongoDB.Driver;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Userservice.WebApi
{
    internal static class StartupExtensions
    {
        public static ISettingsProvider SetupSettings(this IServiceCollection services)
        {
            ISettingsProvider settingsProvider = SettingsProviderFactory.Create();
            services.AddSingleton<ISettingsProvider>(settingsProvider);
            return settingsProvider;
        }

        public static IServiceCollection SetupSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "1",
                    Title = "File Service",
                    Description = "Microservice for managing files and their metadata.",
                    Contact = new OpenApiContact
                    {
                        Name = "Catalin Bugnar",
                        Email = "catalinbgnr@gmail.com"
                    }
                });
            });
            return services;
        }

        public static IServiceCollection SetupMinioClient(this IServiceCollection services, ISettingsProvider settingsProvider)
        {
            services.AddMinio(configureClient => configureClient
                    .WithEndpoint(settingsProvider.MinioEndpoint)
                    .WithCredentials(settingsProvider.MinioAccesKey, settingsProvider.MinioSecretKey)
                    .WithSSL(false));
            return services;
        }

        public static IServiceCollection SetupDatabase(this IServiceCollection services)
        {
            services.AddSingleton<IMinioNotebookDataProvider, MinioNotebookDataProvider>();
            services.AddSingleton<INotebookDataProvider, NotebookDataProvider>();
            return services;
        }

        public static IServiceCollection SetupDataContext(this IServiceCollection services, ISettingsProvider settingsProvider)
        {
            var mongoDataContext = new MongoDataContext(settingsProvider.ConnectionString);
            services.AddSingleton<IMongoDataContext>(mongoDataContext);
            var mongoClient = new MongoClient(settingsProvider.ConnectionString);
            services.AddSingleton<IMongoClient>(mongoClient);
            return services;
        }

        public static IServiceCollection SetupServices(this IServiceCollection services)
        {
            services.AddSingleton<INotebookFacade, NotebookFacade>();
            return services;
        }

        public static IServiceCollection SetupOpenTelemetry(this IServiceCollection services, ISettingsProvider settingsProvider)
        {
            services.AddOpenTelemetry()
                .WithTracing(builder =>
                {
                    builder
                        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("File Service"))
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation()
                        .AddOtlpExporter(options =>
                        {
                            options.Endpoint = settingsProvider.OtelUrl;
                        })
                        .AddConsoleExporter();
                })
                .WithMetrics(builder =>
                {
                    builder
                        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("File Service"))
                        .AddAspNetCoreInstrumentation()
                        .AddOtlpExporter(options =>
                        {
                            options.Endpoint = settingsProvider.OtelUrl;
                        })
                        .AddConsoleExporter();
                });
            return services;
        }
    }
}
