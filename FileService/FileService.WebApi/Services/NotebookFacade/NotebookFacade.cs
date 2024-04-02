using Fileservice.DataStore.Minio.NotebookDataProvider;
using Fileservice.DataStore.Mongo.NotebookDataProvider;
using Fileservice.Models.Entities;
using Fileservice.Models.Requests;

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

        public async Task<Notebook> UploadNotebookAsync(string notebookName, IFormFile file)
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

        public async Task<Notebook> DeleteNotebook(string notebookFileId)
        {
            var notebook = await _notebookDataProvider.GetAsync(notebook => notebook.Id == notebookFileId);
            await _minioNotebookDataProvider.DeleteNotebookFromMinio(notebook.NotebookName, notebook.BucketName);
            await _notebookDataProvider.DeleteAsync(notebook => notebook.Id == notebookFileId);
            return notebook;
        }

        public async Task<string> DownloadNotebookAsync(MemoryStream memoryStream, string notebookName)
        {
            return await _minioNotebookDataProvider.DownloadNotebookFromMinio(memoryStream, notebookName, "notebook");
        }

        public async Task<IEnumerable<Notebook>> GetAllNotebooksAsync() 
        {
            return await _notebookDataProvider.GetAllAsync(_ => true);
        }
        public async Task<Notebook> GetNotebookByNameAsync(string notebookName)
        {
            return await _notebookDataProvider.GetAsync(notebook => notebook.NotebookName == notebookName);
        }

        public async Task<Notebook> AddTagToNotebookAsync(AddNotebookTagRequest request)
        {
            var notebook = await _notebookDataProvider.GetAsync(notebook => notebook.Id == request.NotebookFileId);
            notebook.NotebookTags = notebook.NotebookTags.Append(request.TagToDelete);
            await _notebookDataProvider.UpdateAsync(notebook => notebook.Id == request.NotebookFileId, notebook);
            return notebook;
        }

        public async Task<Notebook> DeleteTagFromNotebookAsync(DeleteNotebookTagRequest request)
        {
            var notebook = await _notebookDataProvider.GetAsync(notebook => notebook.Id == request.NotebookFileId);
            var newTags = notebook.NotebookTags.ToList();
            newTags.Remove(request.TagToDelete);
            notebook.NotebookTags = newTags;
            await _notebookDataProvider.UpdateAsync(notebook => notebook.Id == request.NotebookFileId, notebook);
            return notebook;
        }

        public async Task<IEnumerable<Notebook>> QueryNotebooksAsync(QueryNotebookRequest request)
        {
            return await _notebookDataProvider.GetAllAsync(notebook => request.Tags.Any(element => notebook.NotebookTags.Contains(element)));
        }

        public async Task<IEnumerable<string>> GetAllTagsAsync()
        {
            return (await _notebookDataProvider.GetAllAsync(_ => true)).SelectMany(notebook => notebook.NotebookTags).Distinct();
        }
    }
}
