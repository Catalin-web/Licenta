﻿using Fileservice.Models.Entities;
using Minio;
using Minio.DataModel.Args;

namespace Fileservice.DataStore.Minio.NotebookDataProvider
{
    public class NotebookDataProvider : INotebookDataProvider
    {
        private readonly IMinioClient _minioClient;
        public NotebookDataProvider(IMinioClient minioClient)
        {
            _minioClient = minioClient;
        }

        public async Task UploadNotebookToMinio(Notebook notebook, string bucketName)
        {
            try
            {
                await MakeBucketIfItDoesNotExists(bucketName);
                PutObjectArgs args = new PutObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject(notebook.Name)
                    .WithStreamData(notebook.MemoryStream)
                    .WithObjectSize(notebook.MemoryStream.Length);
                await _minioClient.PutObjectAsync(args);
                StatObjectArgs stat = new StatObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject(notebook.Name);
                var response = await _minioClient.StatObjectAsync(stat);
            }
            catch (Exception ex)
            {
            }
        }

        public async Task<string> DownloadNotebookFromMinio(MemoryStream memoryStream, string notebookName, string bucketName)
        {
            try
            {
                var statArgs = new StatObjectArgs()
                    .WithObject(notebookName)
                    .WithBucket(bucketName);
                var stat = await _minioClient.StatObjectAsync(statArgs);

                var releaseableFileStreamModel = new ReleaseableFileStreamModel
                {
                    ContentType = stat.ContentType,
                    FileName = notebookName,
                };
                var getObjectArgs = new GetObjectArgs()
                    .WithObject(notebookName)
                    .WithBucket(bucketName)
                    .WithCallbackStream((stream) =>
                    {
                        stream.CopyTo(memoryStream);
                    });
                var response = await _minioClient.GetObjectAsync(getObjectArgs);
                return response.ContentType;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private async Task MakeBucketIfItDoesNotExists(string bucketName)
        {
            BucketExistsArgs bucketExistsArgs = new BucketExistsArgs()
                .WithBucket(bucketName);
            var found = await _minioClient.BucketExistsAsync(bucketExistsArgs);
            if (!found)
            {
                MakeBucketArgs makeBucketArgs = new MakeBucketArgs()
                    .WithBucket(bucketName);
                await _minioClient.MakeBucketAsync(makeBucketArgs);
            }
        }
    }
}
