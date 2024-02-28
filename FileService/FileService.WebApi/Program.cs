using Fileservice.WebApi.Settings;
using Userservice.WebApi;

namespace Server.WebApi
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var settingsProvider = SettingsProviderFactory.Create();

            var baseUri = $"{settingsProvider.BindingAddress}:{settingsProvider.Port}";
            return Host.CreateDefaultBuilder(args)
                .ConfigureLogging(builder =>
                {
                    builder.ClearProviders();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls(baseUri);
                });
        }
    }
}