using NotebookService.Models.Entities.Jobs;
using System.Linq.Expressions;

namespace NotebookService.DataStore.Mongo.Jobs.TriggerNotebookGraphJobsHistoryProvider
{
    public interface ITriggerNotebookGraphJobsHistoryProvider
    {
        Task InsertAsync(TriggerNotebookGraphJobHistoryModel triggerNotebookGraphJobHistoryModel);
        Task UpdateAsync(Expression<Func<TriggerNotebookGraphJobHistoryModel, bool>> match, TriggerNotebookGraphJobHistoryModel triggerNotebookGraphJobHistoryModel);
        Task DeleteAsync(Expression<Func<TriggerNotebookGraphJobHistoryModel, bool>> match);
        Task<TriggerNotebookGraphJobHistoryModel> GetAsync(Expression<Func<TriggerNotebookGraphJobHistoryModel, bool>> match);
        Task<IEnumerable<TriggerNotebookGraphJobHistoryModel>> GetAllAsync(Expression<Func<TriggerNotebookGraphJobHistoryModel, bool>> match);
    }
}
