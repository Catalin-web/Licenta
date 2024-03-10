using Fileservice.Models.Entities;

namespace FileService.Clients
{
    public class FileServiceClient : ClientBaseClass
    {
        private const string UploadNotebookRoute = "fileService/notebook/upload/";
        private const string DownloadNotebookRoute = "fileService/notebook/download/";
        private const string GetAllUploadedNotebooksRoute = "fileService/notebook";
        private const string GetUploadedNotebookByNameRoute = "fileService/notebook/name/";

        public async Task<Notebook> UploadNotebookAsync(string notebookName, byte[] fileContent)
        {
            return await PostAsync<Notebook>(UploadNotebookRoute+notebookName, fileContent);
        }

        public async Task<byte[]> DownloadNotebookAsync(string notebookName)
        {
            return await DownloadAsync(DownloadNotebookRoute + notebookName);
        }

        public async Task<IEnumerable<Notebook>> GetAllUploadedNotebooksAsync()
        {
            return await GetAsync<IEnumerable<Notebook>>(GetAllUploadedNotebooksRoute);
        }

        public async Task<Notebook> GetUploadedNotebookByNameAsync(string notebookName)
        {
            return await GetAsync<Notebook>(GetUploadedNotebookByNameRoute + notebookName);
        }
    }
}
