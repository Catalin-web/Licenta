using NotebookService.Models.Entities.ScheduleNotebook;
using NotebookService.Models.Requests;

namespace NotebookService.WebApi.Services.ScheduleNotebookFacade
{
    public interface IScheduleNotebookFacade
    {
        public Task<ScheduledNotebook> ScheduleNotebook(ScheduleNotebookRequest scheduleNotebookRequest);
        public Task<IEnumerable<ScheduledNotebook>> GetAllScheduledNotebooks();
        public Task<ScheduledNotebook> FinishScheduledNotebook(FinishScheduledNotebookRequest finishScheduledNotebookRequest);
        public Task<ScheduledNotebook> UpdateProgressOfScheduledNotebook(UpdateProgressOfScheduledNotebookRequest updateProgressOfScheduledNotebookRequest);
    }
}
