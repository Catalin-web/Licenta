namespace NotebookService.Models.Responses.Statistics
{
    public class NotebookGraphStatisticsResponse
    {
        public int NumberOfNotebookGraphs { get; set; }
        public int NumberOfInprogressGraphs { get; set; }
        public int NumberOfSuccededGraphs { get; set; }
        public int NumberOfFailedGraphs { get; set; }
        public int NumberOfCreatedNotebooks { get; set; }
        public int NumberOfQueuedNotebooks { get; set; }
        public int NumberOfInProgressNotebooks { get; set; }
        public int NumberOfCompletedNotebooks { get; set; }
        public int NumberOfFailedNotebooks { get; set; }
        public int NumberOfSuccedeNotebooks { get; set; }
    }
}
