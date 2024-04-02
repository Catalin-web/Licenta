namespace Fileservice.Models.Requests
{
    public class AddNotebookTagRequest
    {
        public string NotebookFileId { get; set; }
        public string TagToDelete { get; set; }
    }
}
