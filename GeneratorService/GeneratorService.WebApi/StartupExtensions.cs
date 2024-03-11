using GeneratorService.WebApi.Clients.Ollama;
using GeneratorService.WebApi.Services.GenerateParameters;
using GeneratorService.WebApi.Settings;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace GeneratorService.WebApi
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
                    Title = "Generator Service",
                    Description = "This service help generate things.",
                    Contact = new OpenApiContact
                    {
                        Name = "Catalin Bugnar",
                        Email = "catalinbgnr@gmail.com"
                    }
                });
            });
            return services;
        }

        public static IServiceCollection SetupDatabase(this IServiceCollection services)
        {
            // services.AddSingleton<IScheduleNotebookProvider, ScheduleNotebookProvider>();
            return services;
        }

        public static IServiceCollection SetupDataContext(this IServiceCollection services, ISettingsProvider settingsProvider)
        {
            // var mongoDataContext = new MongoDataContext(settingsProvider.ConnectionString);
            // services.AddSingleton<IMongoDataContext>(mongoDataContext);
            return services;
        }

        public static IServiceCollection SetupServices(this IServiceCollection services, ISettingsProvider settingsProvider)
        {
            services.AddSingleton<IParameterGeneratorFacade, ParameterGeneratorFacade>();
            services.AddHttpClient<IOllamaClient, OllamaClient>(client =>
            {
                client.BaseAddress = settingsProvider.OllamaUrl;
                client.Timeout = TimeSpan.FromHours(1);
            }).ConfigurePrimaryHttpMessageHandler((c) =>
                 new HttpClientHandler()
                 {
                     ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                 }
            );
            return services;
        }

        public static IServiceCollection SetupOpenTelemetry(this IServiceCollection services, ISettingsProvider settingsProvider)
        {
            services.AddOpenTelemetry()
                .WithTracing(builder =>
                {
                    builder
                        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("Generator Service"))
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
                        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("Generator Service"))
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
