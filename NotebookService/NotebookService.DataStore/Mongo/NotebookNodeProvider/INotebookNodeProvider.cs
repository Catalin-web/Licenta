using NotebookService.Models.Entities.NotebookGraph;
using NotebookService.Models.Entities.ScheduleNotebook;
using System.Linq.Expressions;

namespace NotebookService.DataStore.Mongo.NotebookGraphProvider
{
    public interface INotebookNodeProvider
    {
        Task InsertAsync(NotebookNode notebookGraph);
        Task UpdateAsync(Expression<Func<NotebookNode, bool>> match, NotebookNode notebookGraph);
        Task DeleteAsync(Expression<Func<NotebookNode, bool>> match);
        Task<NotebookNode> GetAsync(Expression<Func<NotebookNode, bool>> match);
        Task<IEnumerable<NotebookNode>> GetAllAsync(Expression<Func<NotebookNode, bool>> match);
    }
}
