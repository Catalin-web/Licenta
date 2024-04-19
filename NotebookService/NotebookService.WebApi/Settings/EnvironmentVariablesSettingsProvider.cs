using System.Globalization;

namespace NotebookService.WebApi.Settings
{
    public class EnvironmentVariablesSettingsProvider : ISettingsProvider
    {
        public string BindingAddress
        {
            get
            {
                return Environment.GetEnvironmentVariable("NOTEBOOKSERVICE_BINDING_ADRESS") ?? "http://localhost";
            }
        }

        public int Port
        {
            get
            {
                return int.Parse(Environment.GetEnvironmentVariable("NOTEBOOKSERVICE_PORT") ?? "12700", CultureInfo.InvariantCulture);
            }
        }

        public string ConnectionString
        {
            get
            {
                return Environment.GetEnvironmentVariable("NOTEBOOKSERVICE_CONNECTION_STRING") ?? "mongodb://localhost:27017/app";
            }
        }

        public string JobConnectionString
        {
            get
            {
                return Environment.GetEnvironmentVariable("NOTEBOOKSERVICE_JOB_CONNECTION_STRING") ?? "host=postgres;Port=5432;Database=app;User Id=admin;Password=admin;";
            }
        }

        public TimeSpan ScheduleNotebookDelay
        {
            get
            {
                return TimeSpan.FromSeconds(int.Parse(Environment.GetEnvironmentVariable("NOTEBOOKSERVICE_SCHEDULE_NOTEBOOK_DELAY") ?? "1"));
            }
        }

        public Uri ArgoBaseUrl
        {
            get
            {
                return new Uri(Environment.GetEnvironmentVariable("NOTEBOOKSERVICE_ARGO_BASE_URL") ?? "https://localhost:2746");
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
