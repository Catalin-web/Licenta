using Fileservice.Models.Entities;

namespace Fileservice.WebApi.Services.NotebookFacade
{
    public interface INotebookFacade
    {
        public Task<NotebookWithParametersAndMetadata> UploadNotebook(string notebookName, IFormFile formFile);
        public Task<string> DownloadNotebook(MemoryStream memoryStream, string notebookName);
        public Task<IEnumerable<NotebookWithParametersAndMetadata>> GetAllNotebooks();
        public Task<NotebookWithParametersAndMetadata> GetNotebookByName(string notebookName);
    }
}
