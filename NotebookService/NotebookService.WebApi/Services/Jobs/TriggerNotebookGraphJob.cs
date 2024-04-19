using NotebookService.DataStore.Mongo.Jobs.TriggerNotebookGraphJobsHistoryProvider;
using NotebookService.Models.Entities.Jobs;
using NotebookService.Models.Requests.NotebookGraph;
using NotebookService.WebApi.Services.NotebookGraphFacade;
using Quartz;

namespace NotebookService.WebApi.Services.Jobs
{
    public class TriggerNotebookGraphJob : IJob
    {
        private readonly ILogger<TriggerNotebookJob> _logger;
        private readonly INotebookNodeFacade _notebookNodeFacade;
        private readonly ITriggerNotebookGraphJobsHistoryProvider _triggerNotebookGraphJobsHistoryProvider;

        public TriggerNotebookGraphJob(ILogger<TriggerNotebookJob> logger, INotebookNodeFacade notebookNodeFacade, ITriggerNotebookGraphJobsHistoryProvider triggerNotebookGraphJobsHistoryProvider)
        {
            _logger = logger;
            _notebookNodeFacade = notebookNodeFacade;
            _triggerNotebookGraphJobsHistoryProvider = triggerNotebookGraphJobsHistoryProvider;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var request = (ScheduleNotebookNodeRequest)context.MergedJobDataMap["request"];
            var triggerJobId = (string)context.MergedJobDataMap["triggerJobId"];
            var scheduledNotebook = await _notebookNodeFacade.ScheduleNotebookNode(request);
            var triggerNotebookJobHistory = new TriggerNotebookGraphJobHistoryModel()
            {
                TriggerNotebookGraphJobId = triggerJobId,
                GraphUniqueId = scheduledNotebook.GraphUniqueId,
                TriggerTime = DateTime.UtcNow
            };
            await _triggerNotebookGraphJobsHistoryProvider.InsertAsync(triggerNotebookJobHistory);
        }
    }
}
