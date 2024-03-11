namespace GeneratorService.WebApi.Settings
{
    public interface ISettingsProvider
    {
        string BindingAddress { get; }
        int Port { get; }
        Uri OllamaUrl { get; }
        Uri OtelUrl { get; }
    }
}
