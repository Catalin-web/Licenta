namespace GeneratorService.WebApi.Settings
{
    public interface ISettingsProvider
    {
        string BindingAddress { get; }
        int Port { get; }
        string ConnectionString { get; }
        TimeSpan ScheduleJobDelay { get; }
        Uri OllamaUrl { get; }
        Uri OtelUrl { get; }
        string DefaultOpenSourceModelToGenerateParameters { get; }
    }
}
