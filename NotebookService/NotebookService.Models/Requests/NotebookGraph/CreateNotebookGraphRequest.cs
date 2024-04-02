using NotebookService.Models.Entities.NotebookGraph;

namespace NotebookService.Models.Requests.NotebookGraph
{
    public class CreateNotebookGraphRequest
    {
        public NotebookNodeModel RootNode { get; set; }
    }
}
