using NotebookService.DataStore.Mongo.NotebookGraphProvider;
using NotebookService.DataStore.Mongo.ScheduleNotebookHistoryProvider;
using NotebookService.DataStore.Mongo.ScheduleNotebookProvider;
using NotebookService.Models.Entities.NotebookGraph;
using NotebookService.Models.Entities.ScheduleNotebook;
using NotebookService.Models.Requests;
using NotebookService.Models.Responses.Statistics;

namespace NotebookService.WebApi.Services.ScheduleNotebookFacade
{
    public class ScheduleNotebookFacade : IScheduleNotebookFacade
    {
        private readonly IScheduleNotebookProvider _scheduleNotebookProvider;
        private readonly IScheduleNotebookHistoryProvider _scheduleNotebookHistoryProvider;
        private readonly INotebookNodeProvider _notebookNodeProvider;
        public ScheduleNotebookFacade(IScheduleNotebookProvider scheduleNotebookProvider, IScheduleNotebookHistoryProvider scheduleNotebookHistoryProvider, INotebookNodeProvider notebookNodeProvider)
        {
            _scheduleNotebookProvider = scheduleNotebookProvider;
            _scheduleNotebookHistoryProvider = scheduleNotebookHistoryProvider;
            _notebookNodeProvider = notebookNodeProvider;
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
                ErrorMessage = string.Empty,
                InputParameters = scheduleNotebookRequest.InputParameters,
                InputParametersToGenerate = scheduleNotebookRequest.InputParameterstoGenerate,
                OutputParametersNames = scheduleNotebookRequest.OutputParametersNames,
                OutputParameters = new List<NotebookParameter>(),
                NotebookNodeId = null,
                GraphUniqueId = null
            };
            await _scheduleNotebookProvider.InsertAsync(scheduledNotebook);
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
            scheduledNotebook.ErrorMessage = finishScheduledNotebookRequest.ErrorMessage;
            scheduledNotebook.OutputParameters = finishScheduledNotebookRequest.OutputParameters;
            scheduledNotebook.Progress = Progress.COMPLETED;
            scheduledNotebook.FinishedAt = DateTime.UtcNow;
            await _scheduleNotebookProvider.UpdateAsync(scheduledNotebook => scheduledNotebook.Id == finishScheduledNotebookRequest.ScheduledNotebookId, scheduledNotebook);
            await _scheduleNotebookProvider.DeleteAsync(scheduledNotebook => scheduledNotebook.Id == finishScheduledNotebookRequest.ScheduledNotebookId);
            await _scheduleNotebookHistoryProvider.InsertAsync(scheduledNotebook);
            var notebookNode = await _notebookNodeProvider.GetAsync(notebookNode => notebookNode.Id == scheduledNotebook.NotebookNodeId);
            if (notebookNode != null)
            {
                foreach (var childNodeId in notebookNode.ChildNodeIds)
                {
                    var childNode = await _notebookNodeProvider.GetAsync(notebookNode => notebookNode.Id == childNodeId);
                    var inputParameters = childNode.InputParameters.ToList();
                    inputParameters.AddRange(scheduledNotebook.OutputParameters);
                    var newScheduledNotebook = new ScheduledNotebook()
                    {
                        NotebookName = childNode.NotebookName,
                        CreatedAt = DateTime.UtcNow,
                        FinishedAt = null,
                        Progress = Progress.CREATED,
                        Status = Status.NONE,
                        ErrorMessage = string.Empty,
                        InputParameters = inputParameters,
                        InputParametersToGenerate = childNode.InputParameterstoGenerate,
                        OutputParametersNames = childNode.OutputParametersNames,
                        OutputParameters = new List<NotebookParameter>(),
                        NotebookNodeId = childNodeId,
                        GraphUniqueId = scheduledNotebook.GraphUniqueId
                    };
                    await _scheduleNotebookProvider.InsertAsync(newScheduledNotebook);
                }
            }
            return scheduledNotebook;
        }

        public async Task<ScheduledNotebook> UpdateProgressOfScheduledNotebook(UpdateProgressOfScheduledNotebookRequest updateProgressOfScheduledNotebookRequest)
        {
            var scheduledNotebook = await _scheduleNotebookProvider.GetAsync(scheduledNotebook => scheduledNotebook.Id == updateProgressOfScheduledNotebookRequest.ScheduledNotebookId);
            scheduledNotebook.Progress = updateProgressOfScheduledNotebookRequest.Progress;
            await _scheduleNotebookProvider.UpdateAsync(scheduledNotebook => scheduledNotebook.Id == updateProgressOfScheduledNotebookRequest.ScheduledNotebookId, scheduledNotebook);
            return scheduledNotebook;
        }

        public async Task<ScheduledNotebook> GetAndUpdateFirstScheduledNotebook()
        {
            var firstScheduledNotebook = await _scheduleNotebookProvider.GetAsync(scheduledNotebook => scheduledNotebook.Progress == Progress.CREATED);
            if (firstScheduledNotebook == null)
                return null;
            firstScheduledNotebook.Progress = Progress.QUEUED;
            await _scheduleNotebookProvider.UpdateAsync(scheduledNotebook => scheduledNotebook.Id == firstScheduledNotebook.Id, firstScheduledNotebook);
            return firstScheduledNotebook;
        }

        public async Task<IEnumerable<ScheduledNotebook>> GetAllHistoryOfScheduledNotebook()
        {
            return await _scheduleNotebookHistoryProvider.GetAllAsync(notebook => true);
        }

        public async Task<ScheduledNotebookStatisticsResponse> GetScheduledNotebooksStatistics()
        {
            var allNotebooks = (await GetAllScheduledNotebooks()).ToList();
            allNotebooks.AddRange(await GetAllHistoryOfScheduledNotebook());
            var statistics = new ScheduledNotebookStatisticsResponse()
            {
                NumberOfCreatedNotebooks = 0,
                NumberOfQueuedNotebooks = 0,
                NumberOfInProgressNotebooks = 0,
                NumberOfCompletedNotebooks = 0,
                NumberOfFailedNotebooks = 0,
                NumberOfSuccedeNotebooks = 0,
            };
            foreach (var notebook in  allNotebooks)
            {
                switch (notebook.Progress)
                {
                    case Progress.CREATED:
                        {
                            statistics.NumberOfCreatedNotebooks += 1;
                            break;
                        }
                    case Progress.QUEUED:
                        {
                            statistics.NumberOfQueuedNotebooks += 1;
                            break;
                        }
                    case Progress.IN_PROGRESS:
                        {
                            statistics.NumberOfInProgressNotebooks += 1;
                            break;
                        }
                    case Progress.COMPLETED:
                        {
                            statistics.NumberOfCompletedNotebooks += 1;
                            break;
                        }
                }
                switch (notebook.Status)
                {
                    case Status.FAILED:
                        {
                            statistics.NumberOfFailedNotebooks += 1;
                            break;
                        }
                    case Status.SUCCEDED:
                        {
                            statistics.NumberOfSuccedeNotebooks += 1;
                            break;
                        }
                }
            }
            return statistics;
        }
    }
}
