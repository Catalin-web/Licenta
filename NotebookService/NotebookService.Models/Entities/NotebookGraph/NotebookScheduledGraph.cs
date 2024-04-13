using NotebookService.Models.Entities.ScheduleNotebook;

namespace NotebookService.Models.Entities.NotebookGraph
{
    public class NotebookScheduledGraph
    {
        public ScheduledNotebook ScheduleNotebook { get; set; }

        public IEnumerable<NotebookScheduledGraph> ChildGraphs { get; set; }
    }
}
