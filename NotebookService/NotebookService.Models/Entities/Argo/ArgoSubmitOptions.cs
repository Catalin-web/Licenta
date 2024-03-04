namespace NotebookService.Models.Entities.Argo
{
    public class ArgoSubmitOptions
    {
        public string EntryPoint { get; set; } = "handle";
        public IEnumerable<string> Parameters { get; set; }
        public string Labels { get; set; } = string.Empty;
    }
}
