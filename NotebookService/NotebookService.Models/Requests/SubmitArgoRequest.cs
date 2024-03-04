using NotebookService.Models.Entities.Argo;

namespace NotebookService.Models.Requests
{
    public class SubmitArgoRequest
    {
        public string Namespace { get; set; } = "argo";
        public string ResourceKind { get; set; } = "WorkflowTemplate";
        public string ResourceName { get; set; } = "handle-notebook-workflow";
        public ArgoSubmitOptions SubmitOptions { get; set; }
    }
}
