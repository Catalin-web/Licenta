using Fileservice.Models.Entities;

namespace Fileservice.WebApi.Services.NotebookFacade
{
    public interface INotebookFacade
    {
        public Task<Notebook> UploadNotebook(string notebookName, IFormFile formFile);
        public Task<string> DownloadNotebook(MemoryStream memoryStream, string notebookName);
        public Task<IEnumerable<Notebook>> GetAllNotebooks();
        public Task<Notebook> GetNotebookByName(string notebookName);
    }
}
