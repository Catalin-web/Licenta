using GeneratorService.Models.Requests;
using GeneratorService.WebApi.Services.GenerateParameters;
using GeneratorService.WebApi.Settings;

namespace GeneratorService.WebApi.Services.Jobs
{
    public class PullJobBackgroundService : BackgroundService
    {
        private readonly IPullJobFacade _pullJobFacade;
        private readonly IParameterGeneratorFacade _parameterGeneratorFacade;
        private readonly TimeSpan _scheduleJobDelay;
        private readonly string _defaultOpenSourceModelToGenerateParameters;

        public PullJobBackgroundService(IPullJobFacade pullJobFacade, IParameterGeneratorFacade parameterGeneratorFacade, ISettingsProvider settingsProvider)
        {
            _pullJobFacade = pullJobFacade;
            _parameterGeneratorFacade = parameterGeneratorFacade;
            _scheduleJobDelay = settingsProvider.ScheduleJobDelay;
            _defaultOpenSourceModelToGenerateParameters = settingsProvider.DefaultOpenSourceModelToGenerateParameters;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await PullDefaultOpenSourceModelToGenerateParameters();
            while (!stoppingToken.IsCancellationRequested)
            {
                var queuedJob = await _pullJobFacade.GetQueuedJobAsync();
                if (queuedJob == null)
                {
                    await Task.Delay(_scheduleJobDelay);
                    continue;
                }
                await _pullJobFacade.ExecuteQueuedPullJobAsync(queuedJob);
            }
        }

        private async Task PullDefaultOpenSourceModelToGenerateParameters()
        {
            var request = new OllamaPullModelRequest()
            {
                Name = _defaultOpenSourceModelToGenerateParameters
            };
            await _pullJobFacade.InsertAsync(request);
        }
    }
}
