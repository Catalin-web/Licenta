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
    }
}
