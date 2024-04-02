using NotebookService.Models.Entities.NotebookGraph;
using NotebookService.Models.Entities.ScheduleNotebook;
using NotebookService.Models.Requests.NotebookGraph;
using NotebookService.Models.Responses.NotebookGraph;

namespace NotebookService.WebApi.Services.NotebookGraphFacade
{
    public interface INotebookNodeFacade
    {
        Task<NotebookNode> CreateStartingNode(CreateNotebookGraphRequest request);
        Task<AddDependencyBetweenNodesResponse> AddDependencyBetweenNotebookNodes(AddDependencyBetweenNodesRequest request);
        Task<IEnumerable<NotebookNode>> GetAllNotebookNodes();
        Task<ScheduledNotebook> ScheduleNotebookNode(ScheduleNotebookNodeRequest request);
    }
}
