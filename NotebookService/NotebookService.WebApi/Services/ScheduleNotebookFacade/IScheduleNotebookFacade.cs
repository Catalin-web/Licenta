using NotebookService.Models.Entities.NotebookGraph;
using NotebookService.Models.Entities.ScheduleNotebook;
using NotebookService.Models.Requests;
using NotebookService.Models.Responses.Statistics;

namespace NotebookService.WebApi.Services.ScheduleNotebookFacade
{
    public interface IScheduleNotebookFacade
    {
        Task<ScheduledNotebook> ScheduleNotebook(ScheduleNotebookRequest scheduleNotebookRequest);
        Task<IEnumerable<ScheduledNotebook>> GetAllScheduledNotebooks();
        Task<ScheduledNotebook> FinishScheduledNotebook(FinishScheduledNotebookRequest finishScheduledNotebookRequest);
        Task<ScheduledNotebook> UpdateProgressOfScheduledNotebook(UpdateProgressOfScheduledNotebookRequest updateProgressOfScheduledNotebookRequest);
        Task<ScheduledNotebook> GetAndUpdateFirstScheduledNotebook();
        Task<IEnumerable<ScheduledNotebook>> GetAllHistoryOfScheduledNotebook();
        Task<ScheduledNotebookStatisticsResponse> GetScheduledNotebooksStatistics();
    }
}
