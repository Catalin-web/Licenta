using NotebookService.Models.Entities.NotebookGraph;
using NotebookService.Models.Entities.ScheduleNotebook;
using NotebookService.Models.Requests.NotebookGraph;

namespace NotebookService.WebApi.Services.NotebookGraphFacade
{
    public interface INotebookGraphFacade
    {
        Task<NotebookGraph> CreateNotebookGraph(CreateNotebookGraphRequest request);
        Task<IEnumerable<NotebookGraph>> GetAllNotebookGraph();
        Task<ScheduledNotebook> ScheduleNotebookGraph(ScheduleNotebookGraphRequest request);
        Task<IEnumerable<ScheduledNotebook>> ScheduleNextNotebooks(ScheduleNextNotebooksRequest request);
    }
}
