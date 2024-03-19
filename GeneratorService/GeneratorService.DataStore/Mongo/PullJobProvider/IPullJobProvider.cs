using GeneratorService.Models.Entities.Jobs;
using System.Linq.Expressions;

namespace GeneratorService.DataStore.Mongo.NotebookGraphProvider
{
    public interface IPullJobProvider
    {
        Task InsertAsync(PullJob pullJob);
        Task UpdateAsync(Expression<Func<PullJob, bool>> match, PullJob pullJob);
        Task DeleteAsync(Expression<Func<PullJob, bool>> match);
        Task<PullJob> GetAsync(Expression<Func<PullJob, bool>> match);
        Task<IEnumerable<PullJob>> GetAllAsync(Expression<Func<PullJob, bool>> match);
    }
}
