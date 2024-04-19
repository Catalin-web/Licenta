using NotebookService.Models.Entities.Jobs;
using NotebookService.Models.Requests.Jobs;

namespace NotebookService.WebApi.Services.Jobs
{
    public interface INotebookJobFacade
    {
        Task<TriggerNotebookJobModel> ScheduleNotebookJob(NotebookScheduleJobRequest request);
        Task<IEnumerable<TriggerNotebookJobModel>> GetAllScheduledNotebookJobForUser(string userId);
        Task<IEnumerable<TriggerNotebookJobModel>> GetAllScheduledNotebookJob();
        Task<TriggerNotebookJobModel> DeleteTriggerNotebookJob(string triggerNotebookJobId);
        Task<IEnumerable<TriggerNotebookJobHistoryModel>> GetHistoryForTriggerjob(string triggerNotebookJobId);
    }
}
