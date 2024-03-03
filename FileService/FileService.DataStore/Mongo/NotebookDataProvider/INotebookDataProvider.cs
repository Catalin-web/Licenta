using Fileservice.Models.Entities;
using System.Linq.Expressions;

namespace Fileservice.DataStore.Mongo.NotebookDataProvider
{
    public interface INotebookDataProvider
    {
        Task InsertOrUpdateAsync(Notebook notebook);
        Task UpdateAsync(Expression<Func<Notebook, bool>> match, Notebook notebook);
        Task DeleteAsync(Expression<Func<Notebook, bool>> match);
        Task<Notebook> GetAsync(Expression<Func<Notebook, bool>> match);
        Task<IEnumerable<Notebook>> GetAllAsync(Expression<Func<Notebook, bool>> match);
    }
}
