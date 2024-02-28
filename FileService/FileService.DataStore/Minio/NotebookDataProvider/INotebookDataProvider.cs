using Fileservice.Models.Entities;

namespace Fileservice.DataStore.Minio.NotebookDataProvider
{
    public interface INotebookDataProvider
    {
        public Task UploadNotebookToMinio(Notebook notebook, string bucketName);
        public Task<string> DownloadNotebookFromMinio(MemoryStream memoryStream, string notebookName, string bucketName);
    }
}
