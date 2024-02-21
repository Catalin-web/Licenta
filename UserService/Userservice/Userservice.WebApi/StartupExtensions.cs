using Microsoft.OpenApi.Models;
using Userservice.DataStore;
using Userservice.DataStore.UserProvider;
using Userservice.WebApi.Services.UserFacade;
using Userservice.WebApi.Settings;

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
                    Title = "Users Service",
                    Description = "This service manage users.",
                    Contact = new OpenApiContact
                    {
                        Name = "Catalin Bugnar",
                        Email = "catalinbgnr@gmail.com"
                    }
                });
            });
            return services;
        }

        public static IServiceCollection SetupDataContext(this IServiceCollection services, ISettingsProvider settingsProvider)
        {
            var mongoDataContext = new MongoDataContext(settingsProvider.ConnectionString);
            services.AddSingleton<IMongoDataContext>(mongoDataContext);
            return services;
        }

        public static IServiceCollection SetupDatabase(this IServiceCollection services)
        {
            services.AddSingleton<IUserDataProvider, UserDataProvider>();
            return services;
        }

        public static IServiceCollection SetupServices(this IServiceCollection services)
        {
            services.AddSingleton<IUserFacade, UserFacade>();
            return services;
        }
    }
}
