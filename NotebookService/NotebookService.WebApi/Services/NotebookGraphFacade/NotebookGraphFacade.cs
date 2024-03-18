using NotebookService.DataStore.Mongo.NotebookGraphProvider;
using NotebookService.DataStore.Mongo.ScheduleNotebookHistoryProvider;
using NotebookService.DataStore.Mongo.ScheduleNotebookProvider;
using NotebookService.Models.Entities.NotebookGraph;
using NotebookService.Models.Entities.ScheduleNotebook;
using NotebookService.Models.Requests;
using NotebookService.Models.Requests.NotebookGraph;
using NotebookService.WebApi.Services.ScheduleNotebookFacade;

namespace NotebookService.WebApi.Services.NotebookGraphFacade
{
    public class NotebookGraphFacade : INotebookGraphFacade
    {
        private readonly IScheduleNotebookProvider _scheduleNotebookProvider;
        private readonly IScheduleNotebookHistoryProvider _scheduleNotebookHistoryProvider;
        private readonly INotebookGraphProvider _notebookGraphProvider;
        public NotebookGraphFacade(IScheduleNotebookProvider scheduleNotebookProvider, IScheduleNotebookHistoryProvider scheduleNotebookHistoryProvider, INotebookGraphProvider notebookGraphProvider)
        {
            _scheduleNotebookProvider = scheduleNotebookProvider;
            _scheduleNotebookHistoryProvider = scheduleNotebookHistoryProvider;
            _notebookGraphProvider = notebookGraphProvider;
        }

        public async Task<NotebookGraph> CreateNotebookGraph(CreateNotebookGraphRequest request)
        {
            var notebookGraph = new NotebookGraph()
            {
                StartingNode = request.StartingNotebook,
                Nodes = request.Nodes,
            };
            await _notebookGraphProvider.InsertAsync(notebookGraph);
            return notebookGraph;
        }

        public async Task<IEnumerable<NotebookGraph>> GetAllNotebookGraph()
        {
            return await _notebookGraphProvider.GetAllAsync(notebookGraph => true);
        }

        public async Task<ScheduledNotebook> ScheduleNotebookGraph(ScheduleNotebookGraphRequest request)
        {
            var notebookGraph = await _notebookGraphProvider.GetAsync(notebookGraph => notebookGraph.Id == request.NotebookGraphId);
            var scheduledNotebook = new ScheduledNotebook()
            {
                NotebookName = notebookGraph.StartingNode.NotebookName,
                CreatedAt = DateTime.UtcNow,
                FinishedAt = null,
                Progress = Progress.CREATED,
                Status = Status.NONE,
                InputParameters = notebookGraph.StartingNode.InputParameters,
                InputParametersToGenerate = notebookGraph.StartingNode.InputParameterstoGenerate,
                OutputParametersNames = notebookGraph.StartingNode.OutputParametersNames,
                OutputParameters = new List<NotebookParameter>(),
                NotebookGraphId = request.NotebookGraphId
            };
            await _scheduleNotebookProvider.InsertAsync(scheduledNotebook);
            return scheduledNotebook;
        }

        public async Task<IEnumerable<ScheduledNotebook>> ScheduleNextNotebooks(ScheduleNextNotebooksRequest request)
        {
            var notebookGraph = await _notebookGraphProvider.GetAsync(notebookGraph => notebookGraph.Id == request.NotebookGraphId);
            var currentScheduledNotebook = await _scheduleNotebookHistoryProvider.GetAsync(scheduledNotebook => scheduledNotebook.Id == request.ScheduledNotebookId);
            var notebookNodes = notebookGraph.Nodes[currentScheduledNotebook.NotebookName];
            var scheduledNotebookList = new List<ScheduledNotebook>();
            foreach (var notebookNode in notebookNodes)
            {
                var inputParameters = notebookNode.InputParameters.ToList();
                inputParameters.AddRange(currentScheduledNotebook.OutputParameters);
                var newScheduledNotebook = new ScheduledNotebook()
                {
                    NotebookName = notebookNode.NotebookName,
                    CreatedAt = DateTime.UtcNow,
                    FinishedAt = null,
                    Progress = Progress.CREATED,
                    Status = Status.NONE,
                    InputParameters = inputParameters,
                    InputParametersToGenerate = notebookNode.InputParameterstoGenerate,
                    OutputParametersNames = notebookNode.OutputParametersNames,
                    OutputParameters = new List<NotebookParameter>(),
                    NotebookGraphId = request.NotebookGraphId
                };
                await _scheduleNotebookProvider.InsertAsync(newScheduledNotebook);
                scheduledNotebookList.Add(newScheduledNotebook);
            }
            return scheduledNotebookList;
        }
    }
}
