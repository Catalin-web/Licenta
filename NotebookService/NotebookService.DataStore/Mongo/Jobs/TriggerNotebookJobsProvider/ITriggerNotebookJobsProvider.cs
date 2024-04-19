using NotebookService.Models.Entities.Jobs;
using System.Linq.Expressions;

namespace NotebookService.DataStore.Mongo.Jobs.TriggerNotebookJobsProvider
{
    public interface ITriggerNotebookJobsProvider
    {
        Task InsertAsync(TriggerNotebookJobModel triggerNotebookJobModel);
        Task UpdateAsync(Expression<Func<TriggerNotebookJobModel, bool>> match, TriggerNotebookJobModel triggerNotebookJobModel);
        Task DeleteAsync(Expression<Func<TriggerNotebookJobModel, bool>> match);
        Task<TriggerNotebookJobModel> GetAsync(Expression<Func<TriggerNotebookJobModel, bool>> match);
        Task<IEnumerable<TriggerNotebookJobModel>> GetAllAsync(Expression<Func<TriggerNotebookJobModel, bool>> match);
    }
}
