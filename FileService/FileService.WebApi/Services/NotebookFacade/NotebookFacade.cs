using Fileservice.DataStore.Minio.NotebookDataProvider;
using Fileservice.DataStore.Mongo.NotebookDataProvider;
using Fileservice.Models.Entities;

namespace Fileservice.WebApi.Services.NotebookFacade
{
    public class NotebookFacade : INotebookFacade
    {
        private readonly INotebookDataProvider _notebookDataProvider;
        private readonly INotebookWithParametersAndMetadataDataProvider _notebookWithParametersAndMetadataDataProvider;
        public NotebookFacade(INotebookDataProvider notebookDataProvider, INotebookWithParametersAndMetadataDataProvider notebookWithParametersAndMetadataDataProvider) 
        { 
            _notebookDataProvider = notebookDataProvider;
            _notebookWithParametersAndMetadataDataProvider = notebookWithParametersAndMetadataDataProvider;
        }
        public async Task<NotebookWithParametersAndMetadata> UploadNotebook(string notebookName, IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                memoryStream.Position = 0;
                var notebook = new Notebook()
                {
                    MemoryStream = memoryStream,
                    Name = notebookName
                };
                await _notebookDataProvider.UploadNotebookToMinio(notebook, "notebook");
                var notebookWithParametersAndMetadata = new NotebookWithParametersAndMetadata()
                {
                    NotebookName = notebookName,
                    BucketName = "notebook",
                    NotebookParameters = new List<NotebookParameter>(),
                    NotebookTags = new List<string>()
                };
                await _notebookWithParametersAndMetadataDataProvider.InsertOrUpdateAsync(notebookWithParametersAndMetadata);
                return notebookWithParametersAndMetadata;
            }
        }

        public async Task<string> DownloadNotebook(MemoryStream memoryStream, string notebookName)
        {
            return await _notebookDataProvider.DownloadNotebookFromMinio(memoryStream, notebookName, "notebook");
        }

        public async Task<IEnumerable<NotebookWithParametersAndMetadata>> GetAllNotebooks() 
        {
            return await _notebookWithParametersAndMetadataDataProvider.GetAllAsync(_ => true);
        }
        public async Task<NotebookWithParametersAndMetadata> GetNotebookByName(string notebookName)
        {
            return await _notebookWithParametersAndMetadataDataProvider.GetAsync(notebook => notebook.NotebookName == notebookName);
        }
    }
}
