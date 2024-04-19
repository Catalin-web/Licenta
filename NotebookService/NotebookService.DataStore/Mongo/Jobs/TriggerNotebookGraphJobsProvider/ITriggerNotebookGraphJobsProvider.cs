using NotebookService.Models.Entities.Jobs;
using System.Linq.Expressions;

namespace NotebookService.DataStore.Mongo.Jobs.TriggerNotebookGraphJobsProvider
{
    public interface ITriggerNotebookGraphJobsProvider
    {
        Task InsertAsync(TriggerNotebookGraphJobModel triggerNotebookGraphJob);
        Task UpdateAsync(Expression<Func<TriggerNotebookGraphJobModel, bool>> match, TriggerNotebookGraphJobModel triggerNotebookGraphJob);
        Task DeleteAsync(Expression<Func<TriggerNotebookGraphJobModel, bool>> match);
        Task<TriggerNotebookGraphJobModel> GetAsync(Expression<Func<TriggerNotebookGraphJobModel, bool>> match);
        Task<IEnumerable<TriggerNotebookGraphJobModel>> GetAllAsync(Expression<Func<TriggerNotebookGraphJobModel, bool>> match);
    }
}
