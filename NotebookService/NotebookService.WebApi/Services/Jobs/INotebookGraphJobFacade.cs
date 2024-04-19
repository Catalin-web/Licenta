using NotebookService.Models.Entities.Jobs;
using NotebookService.Models.Requests.Jobs;

namespace NotebookService.WebApi.Services.Jobs
{
    public interface INotebookGraphJobFacade
    {
        Task<TriggerNotebookGraphJobModel> ScheduleNotebookGraphJob(NotebookGraphScheduleJobRequest request);
        Task<IEnumerable<TriggerNotebookGraphJobModel>> GetAllScheduledNotebookGraphJobForUser(string userId);
        Task<IEnumerable<TriggerNotebookGraphJobModel>> GetAllScheduledNotebookGraphJob();
        Task<TriggerNotebookGraphJobModel> DeleteTriggerNotebookGraphJob(string triggerNotebookGraphJobId);
        Task<IEnumerable<TriggerNotebookGraphJobHistoryModel>> GetHistoryForNotebookGraphJob(string triggerNotebookGraphJobId);
    }
}
