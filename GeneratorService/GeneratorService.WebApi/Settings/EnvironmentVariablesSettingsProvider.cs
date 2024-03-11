using System.Globalization;

namespace GeneratorService.WebApi.Settings
{
    public class EnvironmentVariablesSettingsProvider : ISettingsProvider
    {
        public string BindingAddress
        {
            get
            {
                return Environment.GetEnvironmentVariable("GENERATORSERVICE_BINDING_ADRESS") ?? "http://localhost";
            }
        }

        public int Port
        {
            get
            {
                return int.Parse(Environment.GetEnvironmentVariable("GENERATORSERVICE_PORT") ?? "12800", CultureInfo.InvariantCulture);
            }
        }

        public Uri OllamaUrl
        {
            get
            {
                return new Uri(Environment.GetEnvironmentVariable("OLLAMA_URL") ?? "https://localhost:4317");
            }
        }

        public Uri OtelUrl
        {
            get
            {
                return new Uri(Environment.GetEnvironmentVariable("OTEL_URL") ?? "https://localhost:4317");
            }
        }
    }
}
