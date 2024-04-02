using Fileservice.Models.Entities;

namespace Fileservice.DataStore.Minio.NotebookDataProvider
{
    public interface IMinioNotebookDataProvider
    {
        public Task UploadNotebookToMinio(MinioNotebook notebook, string bucketName);
        public Task<string> DownloadNotebookFromMinio(MemoryStream memoryStream, string notebookName, string bucketName);
        public Task DeleteNotebookFromMinio(string notebookName, string bucketName);
    }
}
