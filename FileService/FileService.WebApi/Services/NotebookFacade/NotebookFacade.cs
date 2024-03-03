using Fileservice.DataStore.Minio.NotebookDataProvider;
using Fileservice.DataStore.Mongo.NotebookDataProvider;
using Fileservice.Models.Entities;

namespace Fileservice.WebApi.Services.NotebookFacade
{
    public class NotebookFacade : INotebookFacade
    {
        private readonly INotebookDataProvider _notebookDataProvider;
        private readonly IMinioNotebookDataProvider _minioNotebookDataProvider;
        public NotebookFacade(INotebookDataProvider notebookDataProvider, IMinioNotebookDataProvider minioNotebookDataProvider) 
        { 
            _notebookDataProvider = notebookDataProvider;
            _minioNotebookDataProvider = minioNotebookDataProvider;
        }
        public async Task<Notebook> UploadNotebook(string notebookName, IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                memoryStream.Position = 0;
                var notebook = new MinioNotebook()
                {
                    MemoryStream = memoryStream,
                    Name = notebookName
                };
                await _minioNotebookDataProvider.UploadNotebookToMinio(notebook, "notebook");
                var notebookWithParametersAndMetadata = new Notebook()
                {
                    NotebookName = notebookName,
                    BucketName = "notebook",
                    NotebookTags = new List<string>()
                };
                await _notebookDataProvider.InsertOrUpdateAsync(notebookWithParametersAndMetadata);
                return notebookWithParametersAndMetadata;
            }
        }

        public async Task<string> DownloadNotebook(MemoryStream memoryStream, string notebookName)
        {
            return await _minioNotebookDataProvider.DownloadNotebookFromMinio(memoryStream, notebookName, "notebook");
        }

        public async Task<IEnumerable<Notebook>> GetAllNotebooks() 
        {
            return await _notebookDataProvider.GetAllAsync(_ => true);
        }
        public async Task<Notebook> GetNotebookByName(string notebookName)
        {
            return await _notebookDataProvider.GetAsync(notebook => notebook.NotebookName == notebookName);
        }
    }
}
