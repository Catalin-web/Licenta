using Microsoft.OpenApi.Models;
using NotebookService.DataStore.Mongo;
using NotebookService.DataStore.Mongo.ScheduleNotebookProvider;
using NotebookService.WebApi.Clients.Argo;
using NotebookService.WebApi.Clients.ArgoWorkflowClient;
using NotebookService.WebApi.Services;
using NotebookService.WebApi.Services.ScheduleNotebookFacade;
using NotebookService.WebApi.Settings;

namespace NotebookService.WebApi
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
                    Title = "Notebook Service",
                    Description = "This service help to schedule notebooks.",
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
            services.AddSingleton<IScheduleNotebookProvider, ScheduleNotebookProvider>();
            return services;
        }

        public static IServiceCollection SetupDataContext(this IServiceCollection services, ISettingsProvider settingsProvider)
        {
            var mongoDataContext = new MongoDataContext(settingsProvider.ConnectionString);
            services.AddSingleton<IMongoDataContext>(mongoDataContext);
            return services;
        }
         
        public static IServiceCollection SetupServices(this IServiceCollection services, ISettingsProvider settingsProvider)
        {
            services.AddSingleton<IScheduleNotebookFacade, ScheduleNotebookFacade>();
            services.AddHttpClient<IArgoWorkflowClient, ArgoWorkflowClient>(client =>
            {
                client.BaseAddress = settingsProvider.ArgoBaseUrl;
            }).ConfigurePrimaryHttpMessageHandler((c) =>
                 new HttpClientHandler()
                 {
                     ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                 }
            );
            return services;
        }

        public static IServiceCollection SetupBackgroundServices(this IServiceCollection services)
        {
            services.AddHostedService<ScheduleNotebookBackgroundService>();
            return services;
        }
    }
}
