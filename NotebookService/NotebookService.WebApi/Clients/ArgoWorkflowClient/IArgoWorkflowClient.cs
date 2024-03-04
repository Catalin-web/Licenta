namespace NotebookService.WebApi.Clients.ArgoWorkflowClient
{
    public interface IArgoWorkflowClient
    {
        Task ScheduleExecution(string scheduleExecutionId);
    }
}
