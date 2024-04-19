using NotebookService.DataStore.Mongo.Jobs.TriggerNotebookJobsHistoryProvider;
using NotebookService.Models.Entities.Jobs;
using NotebookService.Models.Requests;
using NotebookService.WebApi.Services.ScheduleNotebookFacade;
using Quartz;

namespace NotebookService.WebApi.Services.Jobs
{
    public class TriggerNotebookJob : IJob
    {
        private readonly ILogger<TriggerNotebookJob> _logger;
        private readonly IScheduleNotebookFacade _scheduleNotebookFacade;
        private readonly ITriggerNotebookJobsHistoryProvider _triggerNotebookJobsHistoryProvider;

        public TriggerNotebookJob(ILogger<TriggerNotebookJob> logger, IScheduleNotebookFacade scheduleNotebookFacade, ITriggerNotebookJobsHistoryProvider triggerNotebookJobsHistoryProvider)
        {
            _logger = logger;
            _scheduleNotebookFacade = scheduleNotebookFacade;
            _triggerNotebookJobsHistoryProvider = triggerNotebookJobsHistoryProvider;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var request = (ScheduleNotebookRequest)context.MergedJobDataMap["request"];
            var triggerJobId = (string)context.MergedJobDataMap["triggerJobId"];
            var scheduledNotebook = await _scheduleNotebookFacade.ScheduleNotebook(request);
            var triggerNotebookJobHistory = new TriggerNotebookJobHistoryModel()
            {
                TriggerNotebookJobId = triggerJobId,
                ScheduledNotebookId = scheduledNotebook.Id,
                TriggerTime = DateTime.UtcNow
            };
            await _triggerNotebookJobsHistoryProvider.InsertAsync(triggerNotebookJobHistory);
        }
    }
}
