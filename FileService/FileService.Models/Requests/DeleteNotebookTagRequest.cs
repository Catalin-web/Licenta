namespace Fileservice.Models.Requests
{
    public class DeleteNotebookTagRequest
    {
        public string NotebookFileId { get; set; }
        public string TagToDelete { get; set; }
    }
}
