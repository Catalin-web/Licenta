using System.Globalization;

namespace Fileservice.WebApi.Settings
{
    public class EnvironmentVariablesSettingsProvider : ISettingsProvider
    {
        public string BindingAddress
        {
            get
            {
                return Environment.GetEnvironmentVariable("FILESERVICE_BINDING_ADRESS") ?? "http://localhost";
            }
        }

        public int Port
        {
            get
            {
                return int.Parse(Environment.GetEnvironmentVariable("FILESERVICE_PORT") ?? "12600", CultureInfo.InvariantCulture);
            }
        }

        public string ConnectionString
        {
            get
            {
                return Environment.GetEnvironmentVariable("FILESERVICE_CONNECTION_STRING") ?? "mongodb://localhost:27017/app";
            }
        }

        public string MinioEndpoint
        {
            get
            {
                return Environment.GetEnvironmentVariable("FILESERVICE_MINIO_ENDPOINT") ?? "http://localhost";
            }
        }

        public string MinioAccesKey
        {
            get
            {
                return Environment.GetEnvironmentVariable("FILESERVICE_MINIO_ACCESS_KEY") ?? "access_key";
            }
        }

        public string MinioSecretKey
        {
            get
            {
                return Environment.GetEnvironmentVariable("FILESERVICE_MINIO_SECRET_KEY") ?? "secret_key";
            }
        }
    }
}
