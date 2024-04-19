namespace NotebookService.WebApi.Settings
{
    public interface ISettingsProvider
    {
        string BindingAddress { get; }
        int Port { get; }
        string ConnectionString { get; }
        string JobConnectionString { get; }
        TimeSpan ScheduleNotebookDelay { get; }
        Uri ArgoBaseUrl { get; }
        Uri OtelUrl { get; }
    }
}
