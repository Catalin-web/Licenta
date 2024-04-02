using Microsoft.AspNetCore.Mvc.Routing;
using NotebookService.DataStore.Mongo.NotebookGraphProvider;
using NotebookService.DataStore.Mongo.ScheduleNotebookProvider;
using NotebookService.Models.Entities.NotebookGraph;
using NotebookService.Models.Entities.ScheduleNotebook;
using NotebookService.Models.Requests.NotebookGraph;
using NotebookService.Models.Responses.NotebookGraph;

namespace NotebookService.WebApi.Services.NotebookGraphFacade
{
    public class NotebookNodeFacade : INotebookNodeFacade
    {
        private readonly IScheduleNotebookProvider _scheduleNotebookProvider;
        private readonly INotebookNodeProvider _notebookNodeProvider;
        public NotebookNodeFacade(IScheduleNotebookProvider scheduleNotebookProvider, INotebookNodeProvider notebookGraphProvider)
        {
            _scheduleNotebookProvider = scheduleNotebookProvider;
            _notebookNodeProvider = notebookGraphProvider;
        }

        public async Task<NotebookNode> CreateStartingNode(CreateNotebookNodeRequest request)
        {
            var notebookGraph = new NotebookNode()
            {
                NotebookName = request.NotebookName,
                InputParameters = request.InputParameters,
                InputParameterstoGenerate = request.InputParameterstoGenerate,
                OutputParametersNames = request.OutputParametersNames,
                ChildNodeIds = new List<string>(),
                ParentNodeId = null,
                StartingNodeId = null
            };
            await _notebookNodeProvider.InsertAsync(notebookGraph);
            return notebookGraph;
        }

        public async Task<IEnumerable<NotebookNode>> GetAllNotebookNodes()
        {
            return await _notebookNodeProvider.GetAllAsync(notebookGraph => true);
        }

        public async Task<NotebookNode> GetNotebookNodeById(string notebookNodeId)
        {
            return await _notebookNodeProvider.GetAsync(notebookNode => notebookNode.Id == notebookNodeId);
        }

        public async Task<IEnumerable<NotebookNode>> GetAllChildNotebookNodes(string notebookNodeId)
        {
            var notebookNodeIds = (await _notebookNodeProvider.GetAsync(notebookNode => notebookNode.Id == notebookNodeId)).ChildNodeIds;
            var childNotebookNodes = new List<NotebookNode>();
            foreach (var childNodeId in notebookNodeIds)
            {
                var childNode = await _notebookNodeProvider.GetAsync(notebookNode => notebookNode.Id == childNodeId);
                childNotebookNodes.Add(childNode);
            }
            return childNotebookNodes;
        }

        public async Task<NotebookGraph> GetNotebookGraph(string notebookNodeId)
        {
            var notebookNode = await _notebookNodeProvider.GetAsync(notebookNode => notebookNode.Id == notebookNodeId);
            var childGraphs = new List<NotebookGraph>();
            foreach (var childNotebookNodeId in notebookNode.ChildNodeIds)
            {
                var childGraph = await GetNotebookGraph(childNotebookNodeId);
                childGraphs.Add(childGraph);
            }
            return new NotebookGraph()
            {
                NotebookNode = notebookNode,
                ChildGraphs = childGraphs
            };
        }

        public async Task<NotebookGraph> DeleteNotebookGraph(string notebookNodeId)
        {
            var notebookNode = await _notebookNodeProvider.GetAsync(notebookNode => notebookNode.Id == notebookNodeId);
            await _notebookNodeProvider.DeleteAsync(notebookNode => notebookNode.Id == notebookNodeId);
            var childGraphs = new List<NotebookGraph>();
            foreach (var childNotebookNodeId in notebookNode.ChildNodeIds)
            {
                var childGraph = await DeleteNotebookGraph(childNotebookNodeId);
                childGraphs.Add(childGraph);
            }
            return new NotebookGraph()
            {
                NotebookNode = notebookNode,
                ChildGraphs = childGraphs
            };
        }

        public async Task<AddDependencyBetweenNodesResponse> AddDependencyBetweenNotebookNodes(AddDependencyBetweenNodesRequest request)
        {
            var childNode = await _notebookNodeProvider.GetAsync(notebookNode => notebookNode.Id == request.ChildNodeId);
            var parentNode = await _notebookNodeProvider.GetAsync(notebookNode => notebookNode.Id == request.ParentNodeId);
            childNode.ParentNodeId = parentNode.Id;
            if (parentNode.StartingNodeId != null)
            {
                childNode.StartingNodeId = parentNode.StartingNodeId;
            }
            else
            {
                childNode.StartingNodeId = parentNode.Id;
            }
            parentNode.ChildNodeIds = parentNode.ChildNodeIds.Append(childNode.Id);
            await _notebookNodeProvider.UpdateAsync(notebookNode => notebookNode.Id == request.ChildNodeId, childNode);
            await _notebookNodeProvider.UpdateAsync(notebookNode => notebookNode.Id == request.ParentNodeId, parentNode);
            return new AddDependencyBetweenNodesResponse()
            {
                ChildNode = childNode,
                ParentNode = parentNode
            };
        }

        public async Task<ScheduledNotebook> ScheduleNotebookNode(ScheduleNotebookNodeRequest request)
        {
            var notebookNode = await _notebookNodeProvider.GetAsync(notebookGraph => notebookGraph.Id == request.NotebookNodeId);
            var scheduledNotebook = new ScheduledNotebook()
            {
                NotebookName = notebookNode.NotebookName,
                CreatedAt = DateTime.UtcNow,
                FinishedAt = null,
                Progress = Progress.CREATED,
                Status = Status.NONE,
                InputParameters = notebookNode.InputParameters,
                InputParametersToGenerate = notebookNode.InputParameterstoGenerate,
                OutputParametersNames = notebookNode.OutputParametersNames,
                OutputParameters = new List<NotebookParameter>(),
                NotebookNodeId = request.NotebookNodeId
            };
            await _scheduleNotebookProvider.InsertAsync(scheduledNotebook);
            return scheduledNotebook;
        }

        public async Task<IEnumerable<NotebookNode>> GetAllStartingNotebookNodes()
        {
            return await _notebookNodeProvider.GetAllAsync(notebookNode => notebookNode.StartingNodeId == null);
        }

        public async Task<NotebookGraph> CreateNotebookGraph(NotebookNodeModel notebookNodeModel, string parentNotebookNodeId = null)
        {
            var createNotebookNodeRequest = new CreateNotebookNodeRequest()
            {
                NotebookName = notebookNodeModel.NotebookName,
                InputParameters = notebookNodeModel.InputParameters,
                InputParameterstoGenerate = notebookNodeModel.InputParameterstoGenerate,
                OutputParametersNames = notebookNodeModel.OutputParametersNames
            };
            var notebookNode = await CreateStartingNode(createNotebookNodeRequest);
            if (parentNotebookNodeId != null) 
            {
                var addDependencyRequest = new AddDependencyBetweenNodesRequest()
                {
                    ParentNodeId = parentNotebookNodeId,
                    ChildNodeId = notebookNode.Id
                };
                await AddDependencyBetweenNotebookNodes(addDependencyRequest);
            }
            var childGraphs = new List<NotebookGraph>();
            foreach(var childNotebookModel in notebookNodeModel.ChildNodes)
            {
                childGraphs.Add(await CreateNotebookGraph(childNotebookModel, notebookNode.Id));
            }
            return new NotebookGraph()
            {
                NotebookNode = notebookNode,
                ChildGraphs = childGraphs
            };
        }
    }
}
