using NotebookService.Models.Entities.ScheduleNotebook;

namespace NotebookService.Models.Requests.NotebookGraph
{
    public class ScheduleNextNotebooksRequest
    {
        public string NotebookGraphId { get; set; }
        public string ScheduledNotebookId { get; set; }
    }
}
