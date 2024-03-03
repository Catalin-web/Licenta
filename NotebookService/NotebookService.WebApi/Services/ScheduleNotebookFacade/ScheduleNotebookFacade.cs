using NotebookService.DataStore.Mongo.ScheduleNotebookProvider;
using NotebookService.Models.Entities.ScheduleNotebook;
using NotebookService.Models.Requests;

namespace NotebookService.WebApi.Services.ScheduleNotebookFacade
{
    public class ScheduleNotebookFacade : IScheduleNotebookFacade
    {
        private readonly IScheduleNotebookProvider _scheduleNotebookProvider;
        public ScheduleNotebookFacade(IScheduleNotebookProvider scheduleNotebookProvider)
        {
            _scheduleNotebookProvider = scheduleNotebookProvider;
        }

        public async Task<ScheduledNotebook> ScheduleNotebook(ScheduleNotebookRequest scheduleNotebookRequest)
        {
            var scheduledNotebook = new ScheduledNotebook()
            {
                NotebookName = scheduleNotebookRequest.NotebookName,
                CreatedAt = DateTime.UtcNow,
                FinishedAt = null,
                Progress = Progress.CREATED,
                Status = Status.NONE,
                InputParameters = scheduleNotebookRequest.InputParameters,
                OutputParametersNames = scheduleNotebookRequest.OutputParametersNames,
                OutputParameters = new List<NotebookParameter>()
            };
            await _scheduleNotebookProvider.InsertOrUpdateAsync(scheduledNotebook);
            return scheduledNotebook;
        }

        public async Task<IEnumerable<ScheduledNotebook>> GetAllScheduledNotebooks()
        {
            return await _scheduleNotebookProvider.GetAllAsync(_ => true);
        }

        public async Task<ScheduledNotebook> FinishScheduledNotebook(FinishScheduledNotebookRequest finishScheduledNotebookRequest)
        {
            var scheduledNotebook = await _scheduleNotebookProvider.GetAsync(scheduledNotebook => scheduledNotebook.Id == finishScheduledNotebookRequest.ScheduledNotebookId);
            scheduledNotebook.Status = finishScheduledNotebookRequest.Status;
            scheduledNotebook.OutputParameters = finishScheduledNotebookRequest.OutputParameters;
            scheduledNotebook.Progress = Progress.COMPLETED;
            scheduledNotebook.FinishedAt = DateTime.UtcNow;
            await _scheduleNotebookProvider.UpdateAsync(scheduledNotebook => scheduledNotebook.Id == finishScheduledNotebookRequest.ScheduledNotebookId, scheduledNotebook);
            return scheduledNotebook;
        }

        public async Task<ScheduledNotebook> UpdateProgressOfScheduledNotebook(UpdateProgressOfScheduledNotebookRequest updateProgressOfScheduledNotebookRequest)
        {
            var scheduledNotebook = await _scheduleNotebookProvider.GetAsync(scheduledNotebook => scheduledNotebook.Id == updateProgressOfScheduledNotebookRequest.ScheduledNotebookId);
            scheduledNotebook.Progress = updateProgressOfScheduledNotebookRequest.Progress;
            await _scheduleNotebookProvider.UpdateAsync(scheduledNotebook => scheduledNotebook.Id == updateProgressOfScheduledNotebookRequest.ScheduledNotebookId, scheduledNotebook);
            return scheduledNotebook;
        }
    }
}
