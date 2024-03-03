using NotebookService.Models.Entities.ScheduleNotebook;

namespace NotebookService.Models.Requests
{
    public class FinishScheduledNotebookRequest
    {
        public string ScheduledNotebookId { get; set; }
        public Status Status { get; set; }
        public IEnumerable<NotebookParameter> OutputParameters { get; set; }
    }
}
