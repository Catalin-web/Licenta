using System.Globalization;

namespace Userservice.WebApi.Settings
{
    public class EnvironmentVariablesSettingsProvider : ISettingsProvider
    {
        public string BindingAddress
        {
            get
            {
                return Environment.GetEnvironmentVariable("USERSERVICE_BINDING_ADRESS") ?? "http://localhost";
            }
        }

        public int Port
        {
            get
            {
                return int.Parse(Environment.GetEnvironmentVariable("USERSERVICE_PORT") ?? "12500", CultureInfo.InvariantCulture);
            }
        }

        public string ConnectionString
        {
            get
            {
                return Environment.GetEnvironmentVariable("USERSERVICE_CONNECTION_STRING") ?? "mongodb://localhost:27017/app";
            }
        }
    }
}
