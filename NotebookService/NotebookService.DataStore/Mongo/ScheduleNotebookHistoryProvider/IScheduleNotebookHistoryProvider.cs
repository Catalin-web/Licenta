using NotebookService.Models.Entities.ScheduleNotebook;
using System.Linq.Expressions;

namespace NotebookService.DataStore.Mongo.ScheduleNotebookHistoryProvider
{
    public interface IScheduleNotebookHistoryProvider
    {
        Task InsertAsync(ScheduledNotebook scheduledNotebook);
        Task UpdateAsync(Expression<Func<ScheduledNotebook, bool>> match, ScheduledNotebook scheduledNotebook);
        Task DeleteAsync(Expression<Func<ScheduledNotebook, bool>> match);
        Task<ScheduledNotebook> GetAsync(Expression<Func<ScheduledNotebook, bool>> match);
        Task<IEnumerable<ScheduledNotebook>> GetAllAsync(Expression<Func<ScheduledNotebook, bool>> match);
    }
}
