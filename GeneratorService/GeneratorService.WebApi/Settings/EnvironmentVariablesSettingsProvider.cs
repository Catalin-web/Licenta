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

        public string ConnectionString
        {
            get
            {
                return Environment.GetEnvironmentVariable("GENERATORSERVICE_CONNECTION_STRING") ?? "mongodb://localhost:27017/app";
            }
        }

        public TimeSpan ScheduleJobDelay
        {
            get
            {
                return TimeSpan.FromSeconds(int.Parse(Environment.GetEnvironmentVariable("GENERATORSERVICE_SCHEDULE_JOB_DELAY_SECONDS") ?? "1"));
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

        public string DefaultOpenSourceModelToGenerateParameters
        {
            get
            {
                return Environment.GetEnvironmentVariable("GENERATORSERVICE_DEFAULT_OPEN_SOURCE_MODEL_TO_GENERATE_PARAMETERS") ?? "phi";
            }
        }
    }
}
