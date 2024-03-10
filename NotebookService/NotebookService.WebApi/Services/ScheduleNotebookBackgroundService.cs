using NotebookService.WebApi.Clients.ArgoWorkflowClient;
using NotebookService.WebApi.Services.ScheduleNotebookFacade;
using NotebookService.WebApi.Settings;

namespace NotebookService.WebApi.Services
{
    public class ScheduleNotebookBackgroundService : BackgroundService
    {
        private readonly IScheduleNotebookFacade _scheduleNotebookFacade;
        private readonly IArgoWorkflowClient _argoWorkflowClient;
        private readonly TimeSpan _scheduleNotebookDelay;
        private readonly ILogger<ScheduleNotebookBackgroundService> _logger;
        public ScheduleNotebookBackgroundService(ILogger<ScheduleNotebookBackgroundService> logger, IScheduleNotebookFacade scheduleNotebookFacade,IArgoWorkflowClient argoWorkflowClient, ISettingsProvider settingsProvider) 
        {
            _scheduleNotebookFacade = scheduleNotebookFacade;
            _argoWorkflowClient = argoWorkflowClient;
            _scheduleNotebookDelay = settingsProvider.ScheduleNotebookDelay;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var firstScheduleNotebook = await _scheduleNotebookFacade.GetAndUpdateFirstScheduledNotebook();
                    if (firstScheduleNotebook == null)
                    {
                        await Task.Delay(_scheduleNotebookDelay);
                        continue;
                    }
                    await _argoWorkflowClient.ScheduleExecution(firstScheduleNotebook.Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }
        }
    }
}
