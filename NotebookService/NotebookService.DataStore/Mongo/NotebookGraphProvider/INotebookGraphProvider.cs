using NotebookService.Models.Entities.NotebookGraph;
using NotebookService.Models.Entities.ScheduleNotebook;
using System.Linq.Expressions;

namespace NotebookService.DataStore.Mongo.NotebookGraphProvider
{
    public interface INotebookGraphProvider
    {
        Task InsertAsync(NotebookGraph notebookGraph);
        Task UpdateAsync(Expression<Func<NotebookGraph, bool>> match, NotebookGraph notebookGraph);
        Task DeleteAsync(Expression<Func<NotebookGraph, bool>> match);
        Task<NotebookGraph> GetAsync(Expression<Func<NotebookGraph, bool>> match);
        Task<IEnumerable<NotebookGraph>> GetAllAsync(Expression<Func<NotebookGraph, bool>> match);
    }
}
