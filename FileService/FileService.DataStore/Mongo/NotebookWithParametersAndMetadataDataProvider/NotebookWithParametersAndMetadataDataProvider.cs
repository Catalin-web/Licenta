using Fileservice.Models.Entities;
using MongoDB.Driver;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Fileservice.DataStore.Mongo.NotebookDataProvider
{
    public class NotebookWithParametersAndMetadataDataProvider : INotebookWithParametersAndMetadataDataProvider
    {
        private readonly IMongoDataContext _context;
        private readonly IMongoClient _mongoClient;
        private readonly string _collectionName = "Notebook";

        public NotebookWithParametersAndMetadataDataProvider(IMongoDataContext context, IMongoClient mongoClient)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mongoClient = mongoClient ?? throw new ArgumentNullException(nameof(mongoClient));
        }

        protected IMongoCollection<NotebookWithParametersAndMetadata> Collection => _context.Database.GetCollection<NotebookWithParametersAndMetadata>(_collectionName);

        public async Task InsertOrUpdateAsync(NotebookWithParametersAndMetadata notebookWithParametersAndMetadata)
        {
            var alreadyExistingItem = await GetAsync(notebook => notebook.NotebookName == notebookWithParametersAndMetadata.NotebookName && notebook.BucketName == notebookWithParametersAndMetadata.BucketName);
            if (alreadyExistingItem != null)
            {
                await UpdateAsync(notebook => notebook.NotebookName == notebookWithParametersAndMetadata.NotebookName && notebook.BucketName == notebookWithParametersAndMetadata.BucketName, notebookWithParametersAndMetadata);
            }
            {
                await Collection.InsertOneAsync(notebookWithParametersAndMetadata);
            }
        }

        public async Task UpdateAsync(Expression<Func<NotebookWithParametersAndMetadata, bool>> match, NotebookWithParametersAndMetadata notebookWithParametersAndMetadata)
        {
            var alreadyExistingItem = await GetAsync(notebook => notebook.NotebookName == notebookWithParametersAndMetadata.NotebookName && notebook.BucketName == notebookWithParametersAndMetadata.BucketName);
            if (alreadyExistingItem != null)
            {
                notebookWithParametersAndMetadata.Id = alreadyExistingItem.Id;
                await Collection.ReplaceOneAsync(match, notebookWithParametersAndMetadata);
            }
        }

        public async Task<NotebookWithParametersAndMetadata> GetAsync(Expression<Func<NotebookWithParametersAndMetadata, bool>> match)
        {
            var notebookWithParametersAndMetadata = await (await Collection.FindAsync(match)).FirstOrDefaultAsync();
            return notebookWithParametersAndMetadata;
        }

        public async Task<IEnumerable<NotebookWithParametersAndMetadata>> GetAllAsync(Expression<Func<NotebookWithParametersAndMetadata, bool>> match)
        {
            return await (await Collection.FindAsync(match)).ToListAsync();
        }

        public async Task DeleteAsync(Expression<Func<NotebookWithParametersAndMetadata, bool>> match)
        {
            await Collection.DeleteManyAsync(match);
        }
    }
}
