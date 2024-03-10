using NotebookService.Models.Entities.ScheduleNotebook;
using NotebookService.Models.Requests;

namespace NotebookService.Clients
{
    public class ScheduledNotebookServiceClient : ClientBaseClass
    {
        private const string ScheduleNotebookRoute = "notebookService/scheduleNotebook/schedule";
        private const string GetAllScheduledNotebookRoute = "notebookService/scheduleNotebook";
        private const string FinishScheduledNotebookRoute = "notebookService/scheduleNotebook/finish";
        private const string UpdateStatusScheduledNotebookRoute = "notebookService/scheduleNotebook/updateStatus";

        public async Task<ScheduledNotebook> ScheduleNotebookAsync(ScheduleNotebookRequest request)
        {
            return await PostAsync<ScheduledNotebook>(request, ScheduleNotebookRoute);
        }

        public async Task<IEnumerable<ScheduledNotebook>> GetAllScheduledNotebookAsync()
        {
            return await GetAsync<IEnumerable<ScheduledNotebook>>(GetAllScheduledNotebookRoute);
        }

        public async Task<ScheduledNotebook> FinishScheduledNotebookAsync(FinishScheduledNotebookRequest finishScheduledNotebookRequest)
        {
            return await PostAsync<ScheduledNotebook>(finishScheduledNotebookRequest, FinishScheduledNotebookRoute);
        }

        public async Task<ScheduledNotebook> UpdateProgressOfScheaduledNotebookAsync(UpdateProgressOfScheduledNotebookRequest updateProgressOfScheduledNotebookRequest)
        {
            return await PostAsync<ScheduledNotebook>(updateProgressOfScheduledNotebookRequest, UpdateStatusScheduledNotebookRoute);
        }
    }
}
