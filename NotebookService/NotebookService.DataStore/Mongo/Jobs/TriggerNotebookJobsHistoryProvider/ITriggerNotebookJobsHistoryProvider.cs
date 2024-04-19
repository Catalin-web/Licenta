using NotebookService.Models.Entities.Jobs;
using System.Linq.Expressions;

namespace NotebookService.DataStore.Mongo.Jobs.TriggerNotebookJobsHistoryProvider
{
    public interface ITriggerNotebookJobsHistoryProvider
    {
        Task InsertAsync(TriggerNotebookJobHistoryModel triggerNotebookJobModel);
        Task UpdateAsync(Expression<Func<TriggerNotebookJobHistoryModel, bool>> match, TriggerNotebookJobHistoryModel triggerNotebookJobModel);
        Task DeleteAsync(Expression<Func<TriggerNotebookJobHistoryModel, bool>> match);
        Task<TriggerNotebookJobHistoryModel> GetAsync(Expression<Func<TriggerNotebookJobHistoryModel, bool>> match);
        Task<IEnumerable<TriggerNotebookJobHistoryModel>> GetAllAsync(Expression<Func<TriggerNotebookJobHistoryModel, bool>> match);
    }
}
