using GeneratorService.DataStore.Mongo.NotebookGraphProvider;
using GeneratorService.Models.Entities.Jobs;
using GeneratorService.Models.Requests;
using GeneratorService.WebApi.Services.GenerateParameters;

namespace GeneratorService.WebApi.Services.Jobs
{
    public class PullJobFacade : IPullJobFacade
    {
        private readonly IPullJobProvider _pullJobProvider;
        private readonly IParameterGeneratorFacade _parameterGeneratorFacade;

        public PullJobFacade(IPullJobProvider pullJobProvider, IParameterGeneratorFacade parameterGeneratorFacade) 
        {
            _pullJobProvider = pullJobProvider;
            _parameterGeneratorFacade = parameterGeneratorFacade;
        }

        public async Task<IEnumerable<PullJob>> GetAllPullJobsAsync()
        {
            return await _pullJobProvider.GetAllAsync(pullJob => true);
        }

        public async Task<PullJob?> GetQueuedJobAsync()
        {
            return await _pullJobProvider.GetAsync(pullJob => pullJob.Progress == Progress.QUEUED);
        }

        public async Task<PullJob> InsertAsync(OllamaPullModelRequest request)
        {
            var pullJob = new PullJob()
            {
                Model = request.Name,
                JobType = JobType.PULL_MODEL,
                CreatedAt = DateTime.UtcNow,
                FinishedAt = null,
                Progress = Progress.QUEUED,
                Status = Status.NONE
            };
            await _pullJobProvider.InsertAsync(pullJob);
            return pullJob;
        }

        public async Task ExecuteQueuedPullJobAsync(PullJob pullJob)
        {
            pullJob.Progress = Progress.IN_PROGRESS;
            await _pullJobProvider.UpdateAsync(job => job.Id == pullJob.Id, pullJob);
            
            pullJob.Progress = Progress.COMPLETED;
            try
            {
                var request = new OllamaPullModelRequest()
                {
                    Name = pullJob.Model
                };
                var response = await _parameterGeneratorFacade.PullModelAsync(request);
                pullJob.Status = Status.SUCCEDED;
                pullJob.Response = response;
            }
            catch (Exception ex)
            {
                pullJob.Status = Status.FAILED;
            }
            pullJob.FinishedAt = DateTime.UtcNow;
            await _pullJobProvider.UpdateAsync(job => job.Id == pullJob.Id, pullJob);
        }
    }
}
