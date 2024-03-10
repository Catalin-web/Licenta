using Fileservice.Models.Entities;
using FileService.Clients;
using System.Runtime.InteropServices;

namespace FileService.Tests.Integration
{
    public class FileServiceTests
    {
        private static FileServiceClient _fileServiceClient = new FileServiceClient();

        [Fact]
        public async Task UploadFileWorks()
        {
            var notebookName = $"notebookName-{Guid.NewGuid()}";
            var notebook = await _fileServiceClient.UploadNotebookAsync(notebookName, [ 1, 2, 3 ]);

            Assert.NotNull(notebook);
            Assert.Equal(notebookName, notebook.NotebookName);
            Assert.Equal("notebook", notebook.BucketName);
            Assert.Empty(notebook.NotebookTags);
        }

        [Fact]
        public async Task DownloadFileFileWorks()
        {
            var notebookName = $"notebookName-{Guid.NewGuid()}";
            await _fileServiceClient.UploadNotebookAsync(notebookName, [1, 2, 3]);
            var downloadedNotebook = await _fileServiceClient.DownloadNotebookAsync(notebookName);

            Assert.NotNull(downloadedNotebook);
            Assert.Equal([1, 2, 3], downloadedNotebook);
        }

        [Fact]
        public async Task GetAllUploadedNotebooksWorks()
        {
            var notebookName = $"notebookName-{Guid.NewGuid()}";
            await _fileServiceClient.UploadNotebookAsync(notebookName, [1, 2, 3]);
            var notebook = (await _fileServiceClient.GetAllUploadedNotebooksAsync()).First(notebook => notebook.NotebookName == notebookName);

            Assert.NotNull(notebook);
            Assert.Equal(notebookName, notebook.NotebookName);
            Assert.Equal("notebook", notebook.BucketName);
            Assert.Empty(notebook.NotebookTags);
        }
        [Fact]
        public async Task GetUploadedNotebookByNameWorks()
        {
            var notebookName = $"notebookName-{Guid.NewGuid()}";
            await _fileServiceClient.UploadNotebookAsync(notebookName, [1, 2, 3]);
            var notebook = await _fileServiceClient.GetUploadedNotebookByNameAsync(notebookName);

            Assert.NotNull(notebook);
            Assert.Equal(notebookName, notebook.NotebookName);
            Assert.Equal("notebook", notebook.BucketName);
            Assert.Empty(notebook.NotebookTags);
        }

    }
}
