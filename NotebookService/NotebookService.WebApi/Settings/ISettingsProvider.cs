namespace NotebookService.WebApi.Settings
{
    public interface ISettingsProvider
    {
        string BindingAddress { get; }
        int Port { get; }
        string ConnectionString { get; }
        TimeSpan ScheduleNotebookDelay { get; }
        Uri ArgoBaseUrl { get; }
        Uri OtelUrl { get; }
    }
}
