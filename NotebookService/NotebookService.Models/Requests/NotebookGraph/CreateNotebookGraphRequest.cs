using NotebookService.Models.Entities.NotebookGraph;

namespace NotebookService.Models.Requests.NotebookGraph
{
    public class CreateNotebookGraphRequest
    {
        public NotebookNode StartingNotebook { get; set; }
        public IDictionary<string, IEnumerable<NotebookNode>> Nodes { get; set; }
    }
}
