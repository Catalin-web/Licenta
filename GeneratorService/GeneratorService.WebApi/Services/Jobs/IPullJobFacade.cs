using GeneratorService.Models.Entities.Jobs;
using GeneratorService.Models.Requests;

namespace GeneratorService.WebApi.Services.Jobs
{
    public interface IPullJobFacade
    {
        Task<IEnumerable<PullJob>> GetAllPullJobsAsync();
        Task<PullJob> InsertAsync(OllamaPullModelRequest request);
        Task<PullJob?> GetQueuedJobAsync();
        Task ExecuteQueuedPullJobAsync(PullJob pullJob);
    }
}
