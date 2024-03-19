using NotebookService.DataStore.Mongo.NotebookGraphProvider;
using NotebookService.DataStore.Mongo.ScheduleNotebookProvider;
using NotebookService.Models.Entities.NotebookGraph;
using NotebookService.Models.Entities.ScheduleNotebook;
using NotebookService.Models.Requests.NotebookGraph;

namespace NotebookService.WebApi.Services.NotebookGraphFacade
{
    public class NotebookGraphFacade : INotebookGraphFacade
    {
        private readonly IScheduleNotebookProvider _scheduleNotebookProvider;
        private readonly INotebookGraphProvider _notebookGraphProvider;
        public NotebookGraphFacade(IScheduleNotebookProvider scheduleNotebookProvider, INotebookGraphProvider notebookGraphProvider)
        {
            _scheduleNotebookProvider = scheduleNotebookProvider;
            _notebookGraphProvider = notebookGraphProvider;
        }

        public async Task<NotebookGraph> CreateNotebookGraph(CreateNotebookGraphRequest request)
        {
            var notebookGraph = new NotebookGraph()
            {
                RootNode = request.RootNode
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
                NotebookName = notebookGraph.RootNode.NotebookName,
                CreatedAt = DateTime.UtcNow,
                FinishedAt = null,
                Progress = Progress.CREATED,
                Status = Status.NONE,
                InputParameters = notebookGraph.RootNode.InputParameters,
                InputParametersToGenerate = notebookGraph.RootNode.InputParameterstoGenerate,
                OutputParametersNames = notebookGraph.RootNode.OutputParametersNames,
                OutputParameters = new List<NotebookParameter>(),
                ChildNodes = notebookGraph.RootNode.ChildNodes
            };
            await _scheduleNotebookProvider.InsertAsync(scheduledNotebook);
            return scheduledNotebook;
        }
    }
}
