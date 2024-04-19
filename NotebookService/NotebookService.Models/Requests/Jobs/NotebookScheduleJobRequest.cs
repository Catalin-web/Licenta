namespace NotebookService.Models.Requests.Jobs
{
    public class NotebookScheduleJobRequest : ScheduleNotebookRequest
    {
        public string JobName { get; set; }
        public int IntervalInSeconds { get; set; }
        public bool TriggerNow { get; set; }
        public string UserId { get; set; }
    }
}
