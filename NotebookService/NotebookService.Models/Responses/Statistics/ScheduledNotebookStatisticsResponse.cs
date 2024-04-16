namespace NotebookService.Models.Responses.Statistics
{
    public class ScheduledNotebookStatisticsResponse
    {
        public int NumberOfCreatedNotebooks { get; set; }
        public int NumberOfQueuedNotebooks { get; set; }
        public int NumberOfInProgressNotebooks { get; set; }
        public int NumberOfCompletedNotebooks { get; set; }
        public int NumberOfFailedNotebooks { get; set; }
        public int NumberOfSuccedeNotebooks { get; set; }
    }
}
