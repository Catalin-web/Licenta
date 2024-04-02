using Fileservice.Models.Entities;
using Fileservice.Models.Requests;

namespace Fileservice.WebApi.Services.NotebookFacade
{
    public interface INotebookFacade
    {
        Task<Notebook> UploadNotebookAsync(string notebookName, IFormFile formFile);
        Task<string> DownloadNotebookAsync(MemoryStream memoryStream, string notebookName);
        Task<Notebook> DeleteNotebook(string notebookFileId);
        Task<IEnumerable<Notebook>> GetAllNotebooksAsync();
        Task<Notebook> GetNotebookByNameAsync(string notebookName);
        Task<Notebook> AddTagToNotebookAsync(AddNotebookTagRequest request);
        Task<Notebook> DeleteTagFromNotebookAsync(DeleteNotebookTagRequest request);
        Task<IEnumerable<Notebook>> QueryNotebooksAsync(QueryNotebookRequest request);
        Task<IEnumerable<string>> GetAllTagsAsync();
    }
}
