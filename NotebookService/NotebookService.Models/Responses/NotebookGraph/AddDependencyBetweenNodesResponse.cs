using NotebookService.Models.Entities.NotebookGraph;

namespace NotebookService.Models.Responses.NotebookGraph
{
    public class AddDependencyBetweenNodesResponse
    {
        public NotebookNode ParentNode { get; set; }
        public NotebookNode ChildNode { get; set; }
    }
}
