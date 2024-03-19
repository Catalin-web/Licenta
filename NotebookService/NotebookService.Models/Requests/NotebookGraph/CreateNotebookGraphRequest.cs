using NotebookService.Models.Entities.NotebookGraph;

namespace NotebookService.Models.Requests.NotebookGraph
{
    public class CreateNotebookGraphRequest
    {
        public NotebookNode RootNode { get; set; }
    }
}
