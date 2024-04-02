namespace NotebookService.Models.Entities.NotebookGraph
{
    public class NotebookGraph
    {
        public NotebookNode NotebookNode { get; set; }
        public IEnumerable<NotebookGraph> ChildGraphs { get; set; }
    }
}
