using NotebookService.Models.Entities.ScheduleNotebook;

namespace NotebookService.Models.Requests
{
    public class UpdateProgressOfScheduledNotebookRequest
    {
        public string ScheduledNotebookId { get; set; }
        public Progress Progress { get; set; }
    }
}
