using Microsoft.OpenApi.Models;
using NotebookService.DataStore.Mongo;
using NotebookService.DataStore.Mongo.Jobs.TriggerNotebookGraphJobsHistoryProvider;
using NotebookService.DataStore.Mongo.Jobs.TriggerNotebookGraphJobsProvider;
using NotebookService.DataStore.Mongo.Jobs.TriggerNotebookJobsHistoryProvider;
using NotebookService.DataStore.Mongo.Jobs.TriggerNotebookJobsProvider;
using NotebookService.DataStore.Mongo.NotebookGraphProvider;
using NotebookService.DataStore.Mongo.ScheduleNotebookHistoryProvider;
using NotebookService.DataStore.Mongo.ScheduleNotebookProvider;
using NotebookService.WebApi.Clients.Argo;
using NotebookService.WebApi.Clients.ArgoWorkflowClient;
using NotebookService.WebApi.Services;
using NotebookService.WebApi.Services.Jobs;
using NotebookService.WebApi.Services.NotebookGraphFacade;
using NotebookService.WebApi.Services.ScheduleNotebookFacade;
using NotebookService.WebApi.Settings;
using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.AdoJobStore;
using Quartz.Impl.AdoJobStore.Common;
using System.Collections.Specialized;
using System.Data;

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
            services.AddSingleton<IScheduleNotebookHistoryProvider, ScheduleNotebookHistoryProvider>();
            services.AddSingleton<INotebookNodeProvider, NotebookNodeProvider>();
            services.AddSingleton<ITriggerNotebookJobsProvider, TriggerNotebookJobsProvider>();
            services.AddSingleton<ITriggerNotebookJobsHistoryProvider, TriggerNotebookJobsHistoryProvider>();
            services.AddSingleton<ITriggerNotebookGraphJobsProvider, TriggerNotebookGraphJobsProvider>();
            services.AddSingleton<ITriggerNotebookGraphJobsHistoryProvider, TriggerNotebookGraphJobsHistoryProvider>();
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
            services.AddSingleton<INotebookNodeFacade, NotebookNodeFacade>();
            services.AddSingleton<INotebookJobFacade, NotebookJobFacade>();
            services.AddSingleton<INotebookGraphJobFacade, NotebookGraphJobFacade>();
            return services;
        }

        public static IServiceCollection SetupBackgroundServices(this IServiceCollection services)
        {
            services.AddHostedService<ScheduleNotebookBackgroundService>();
            services.AddHostedService<StartingGraphBackgroundService>();
            return services;
        }

        public static IServiceCollection SetupOpenTelemetry(this IServiceCollection services, ISettingsProvider settingsProvider)
        {
            services.AddOpenTelemetry()
                .WithTracing(builder =>
                {
                    builder
                        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("Notebook Service"))
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
                        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("Notebook Service"))
                        .AddAspNetCoreInstrumentation()
                        .AddOtlpExporter(options =>
                        {
                            options.Endpoint = settingsProvider.OtelUrl;
                        })
                        .AddConsoleExporter();
                });
            return  services;
        }

        public static IServiceCollection SetupJobs(this IServiceCollection services, ISettingsProvider settingsProvider)
        {
            services.Configure<QuartzOptions>(options =>
            {
                options.Scheduling.IgnoreDuplicates = true;
                options.Scheduling.OverWriteExistingData = true;
            });
            services.AddQuartz(options =>
            {
                options.SchedulerId = "Scheduler-Core";
                options.UsePersistentStore(options =>
                {
                    options.SetProperty("quartz.jobStore.type", "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz");
                    options.SetProperty("quartz.serializer.type", "json");
                    options.UsePostgres(settingsProvider.JobConnectionString);
                });
                options.UseDefaultThreadPool(tp =>
                {
                    tp.MaxConcurrency = 10;
                });
            });
            services.AddQuartzHostedService(options =>
            {
                options.WaitForJobsToComplete = true;
            });

            return services;
        }
    }
}
