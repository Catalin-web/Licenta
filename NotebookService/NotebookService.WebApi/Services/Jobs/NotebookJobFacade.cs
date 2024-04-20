using Microsoft.VisualBasic;
using NotebookService.DataStore.Mongo.Jobs.TriggerNotebookJobsHistoryProvider;
using NotebookService.DataStore.Mongo.Jobs.TriggerNotebookJobsProvider;
using NotebookService.Models.Entities.Jobs;
using NotebookService.Models.Requests.Jobs;
using NotebookService.WebApi.Settings;
using Quartz;

namespace NotebookService.WebApi.Services.Jobs
{
    public class NotebookJobFacade : INotebookJobFacade
    {
        private readonly ISettingsProvider _settingsProvider;
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly ITriggerNotebookJobsProvider _triggerNotebookJobsProvider;
        private readonly ITriggerNotebookJobsHistoryProvider _triggerNotebookJobsHistoryProvider;

        public NotebookJobFacade(ISettingsProvider settingsProvider, ISchedulerFactory schedulerFactory, ITriggerNotebookJobsProvider triggerNotebookJobsProvider, ITriggerNotebookJobsHistoryProvider triggerNotebookJobsHistoryProvider) 
        {
            _settingsProvider = settingsProvider;
            _schedulerFactory = schedulerFactory;
            _triggerNotebookJobsProvider = triggerNotebookJobsProvider;
            _triggerNotebookJobsHistoryProvider = triggerNotebookJobsHistoryProvider;
        }

        public async Task<TriggerNotebookJobModel> ScheduleNotebookJob(NotebookScheduleJobRequest request)
        {
            var jobId = Guid.NewGuid().ToString();
            var triggerId = Guid.NewGuid().ToString();
            var triggerNotebookJobModel = new TriggerNotebookJobModel()
            {
                JobName = request.JobName,
                NotebookName = request.NotebookName,
                InputParameters = request.InputParameters,
                InputParameterstoGenerate = request.InputParameterstoGenerate,
                OutputParametersNames = request.OutputParametersNames,
                JobId = jobId,
                TriggerId = triggerId,
                TriggerJobInterval = request.IntervalInSeconds,
                UserId = request.UserId
            };

            await _triggerNotebookJobsProvider.InsertAsync(triggerNotebookJobModel);

            var jobData = new JobDataMap();
            jobData.Put("request", request);
            jobData.Put("triggerJobId", triggerNotebookJobModel.Id);
            var job = JobBuilder.Create<TriggerNotebookJob>()
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
            return triggerNotebookJobModel;
        }

        public async Task<IEnumerable<TriggerNotebookJobModel>> GetAllScheduledNotebookJobForUser(string userId)
        {
            return await _triggerNotebookJobsProvider.GetAllAsync(triggerNotebookJob => triggerNotebookJob.UserId == userId);
        }

        public async Task<IEnumerable<TriggerNotebookJobModel>> GetAllScheduledNotebookJob()
        {
            return await _triggerNotebookJobsProvider.GetAllAsync(triggerNotebookJob => true);
        }

        public async Task<TriggerNotebookJobModel> DeleteTriggerNotebookJob(string triggerNotebookJobId)
        {
            var triggerNotebookJob = await _triggerNotebookJobsProvider.GetAsync(triggerNotebookJob => triggerNotebookJob.Id == triggerNotebookJobId);
            var jobKey = new JobKey(triggerNotebookJob.JobId);
            await (await _schedulerFactory.GetScheduler()).DeleteJob(jobKey);
            await _triggerNotebookJobsProvider.DeleteAsync(triggerNotebookJob => triggerNotebookJob.Id == triggerNotebookJobId);
            return triggerNotebookJob;
        }

        public async Task<IEnumerable<TriggerNotebookJobHistoryModel>> GetHistoryForTriggerjob(string triggerNotebookJobId)
        {
            return await _triggerNotebookJobsHistoryProvider.GetAllAsync(triggerNotebookHistory => triggerNotebookHistory.TriggerNotebookJobId == triggerNotebookJobId);
        }
    }
}
