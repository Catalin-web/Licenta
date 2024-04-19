using NotebookService.DataStore.Mongo.Jobs.TriggerNotebookGraphJobsHistoryProvider;
using NotebookService.DataStore.Mongo.Jobs.TriggerNotebookGraphJobsProvider;
using NotebookService.Models.Entities.Jobs;
using NotebookService.Models.Requests.Jobs;
using NotebookService.WebApi.Settings;
using Quartz;

namespace NotebookService.WebApi.Services.Jobs
{
    public class NotebookGraphJobFacade : INotebookGraphJobFacade
    {
        private readonly ISettingsProvider _settingsProvider;
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly ITriggerNotebookGraphJobsProvider _triggerNotebookGraphJobsProvider;
        private readonly ITriggerNotebookGraphJobsHistoryProvider _triggerNotebookGraphJobsHistoryProvider;

        public NotebookGraphJobFacade(ISettingsProvider settingsProvider, ISchedulerFactory schedulerFactory, ITriggerNotebookGraphJobsProvider triggerNotebookGraphJobsProvider, ITriggerNotebookGraphJobsHistoryProvider triggerNotebookGraphJobsHistoryProvider)
        {
            _settingsProvider = settingsProvider;
            _schedulerFactory = schedulerFactory;
            _triggerNotebookGraphJobsProvider = triggerNotebookGraphJobsProvider;
            _triggerNotebookGraphJobsHistoryProvider = triggerNotebookGraphJobsHistoryProvider;
        }

        public async Task<TriggerNotebookGraphJobModel> ScheduleNotebookGraphJob(NotebookGraphScheduleJobRequest request)
        {
            var jobId = Guid.NewGuid().ToString();
            var triggerId = Guid.NewGuid().ToString();
            var triggerNotebookGraphJobModel = new TriggerNotebookGraphJobModel()
            {
                JobName = request.JobName,
                NotebookNodeId = request.NotebookNodeId,
                JobId = jobId,
                TriggerId = triggerId,
                TriggerJobInterval = request.IntervalInSeconds,
                UserId = request.UserId
            };

            await _triggerNotebookGraphJobsProvider.InsertAsync(triggerNotebookGraphJobModel);

            var jobData = new JobDataMap();
            jobData.Put("request", request);
            jobData.Put("triggerJobId", triggerNotebookGraphJobModel.Id);
            var job = JobBuilder.Create<TriggerNotebookGraphJob>()
                    .WithIdentity(jobId)
                    .StoreDurably(true)
                    .SetJobData(jobData)
                    .Build();
            TriggerBuilder triggerBuilder = TriggerBuilder.Create()
                 .WithIdentity(triggerId)
                 .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(request.IntervalInSeconds)
                    .RepeatForever());
            if (request.TriggerNow)
            {
                triggerBuilder.StartNow();
            }
            else
            {
                triggerBuilder.StartAt(DateTime.UtcNow + TimeSpan.FromSeconds(request.IntervalInSeconds));
            }
            await (await _schedulerFactory.GetScheduler()).ScheduleJob(job, triggerBuilder.Build());
            return triggerNotebookGraphJobModel;
        }

        public async Task<IEnumerable<TriggerNotebookGraphJobModel>> GetAllScheduledNotebookGraphJobForUser(string userId)
        {
            return await _triggerNotebookGraphJobsProvider.GetAllAsync(triggerNotebookGraphJob => triggerNotebookGraphJob.UserId == userId);
        }

        public async Task<IEnumerable<TriggerNotebookGraphJobModel>> GetAllScheduledNotebookGraphJob()
        {
            return await _triggerNotebookGraphJobsProvider.GetAllAsync(triggerNotebookGraphJob => true);
        }

        public async Task<TriggerNotebookGraphJobModel> DeleteTriggerNotebookGraphJob(string triggerNotebookJobId)
        {
            var triggerNotebookJob = await _triggerNotebookGraphJobsProvider.GetAsync(triggerNotebookJob => triggerNotebookJob.Id == triggerNotebookJobId);
            var jobKey = new JobKey(triggerNotebookJob.JobId);
            await (await _schedulerFactory.GetScheduler()).DeleteJob(jobKey);
            await _triggerNotebookGraphJobsProvider.DeleteAsync(triggerNotebookJob => triggerNotebookJob.Id == triggerNotebookJobId);
            return triggerNotebookJob;
        }

        public async Task<IEnumerable<TriggerNotebookGraphJobHistoryModel>> GetHistoryForNotebookGraphJob(string triggerNotebookGraphJobId)
        {
            return await _triggerNotebookGraphJobsHistoryProvider.GetAllAsync(triggerNotebookHistory => triggerNotebookHistory.TriggerNotebookGraphJobId == triggerNotebookGraphJobId);
        }
    }
}
