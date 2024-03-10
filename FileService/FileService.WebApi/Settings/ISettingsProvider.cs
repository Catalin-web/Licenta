namespace Fileservice.WebApi.Settings
{
    public interface ISettingsProvider
    {
        string BindingAddress { get; }
        int Port { get; }
        string ConnectionString { get; }
        string MinioEndpoint { get; }
        string MinioAccesKey { get; }
        string MinioSecretKey { get; }
        Uri OtelUrl { get; }
    }
}
