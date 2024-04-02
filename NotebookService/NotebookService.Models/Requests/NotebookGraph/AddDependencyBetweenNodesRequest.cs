namespace NotebookService.Models.Requests.NotebookGraph
{
    public class AddDependencyBetweenNodesRequest
    {
        public string ParentNodeId { get; set; }
        public string ChildNodeId { get; set; }
    }
}
