using NotebookService.Models.Entities.NotebookGraph;
using NotebookService.Models.Entities.ScheduleNotebook;
using NotebookService.Models.Requests.NotebookGraph;
using NotebookService.Models.Responses.NotebookGraph;

namespace NotebookService.WebApi.Services.NotebookGraphFacade
{
    public interface INotebookNodeFacade
    {
        Task<NotebookNode> CreateStartingNode(CreateNotebookNodeRequest request);
        Task<AddDependencyBetweenNodesResponse> AddDependencyBetweenNotebookNodes(AddDependencyBetweenNodesRequest request);
        Task<IEnumerable<NotebookNode>> GetAllNotebookNodes();
        Task<NotebookNode> GetNotebookNodeById(string notebookNodeId);
        Task<IEnumerable<NotebookNode>> GetAllChildNotebookNodes(string notebookNodeId);
        Task<IEnumerable<NotebookNode>> GetAllStartingNotebookNodes();
        Task<NotebookGraph> GetNotebookGraph(string notebookNodeId);
        Task<ScheduledNotebook> ScheduleNotebookNode(ScheduleNotebookNodeRequest request);
        Task<NotebookGraph> DeleteNotebookGraph(string notebookNodeId);
        Task<NotebookGraph> CreateNotebookGraph(NotebookNodeModel notebookNodeModel, string parentNotebookNodeId = null);
    }
}
