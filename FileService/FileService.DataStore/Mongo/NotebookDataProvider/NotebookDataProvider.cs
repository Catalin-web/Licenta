using Fileservice.Models.Entities;
using MongoDB.Driver;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Fileservice.DataStore.Mongo.NotebookDataProvider
{
    public class NotebookDataProvider : INotebookDataProvider
    {
        private readonly IMongoDataContext _context;
        private readonly IMongoClient _mongoClient;
        private readonly string _collectionName = "Notebook";

        public NotebookDataProvider(IMongoDataContext context, IMongoClient mongoClient)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mongoClient = mongoClient ?? throw new ArgumentNullException(nameof(mongoClient));
        }

        protected IMongoCollection<Notebook> Collection => _context.Database.GetCollection<Notebook>(_collectionName);

        public async Task InsertOrUpdateAsync(Notebook notebookToInsert)
        {
            var alreadyExistingItem = await GetAsync(notebook => notebook.NotebookName == notebookToInsert.NotebookName && notebook.BucketName == notebookToInsert.BucketName);
            if (alreadyExistingItem != null)
            {
                await UpdateAsync(notebook => notebook.NotebookName == notebook.NotebookName && notebook.BucketName == notebook.BucketName, notebookToInsert);
            }
            else
            {
                await Collection.InsertOneAsync(notebookToInsert);
            }
        }

        public async Task UpdateAsync(Expression<Func<Notebook, bool>> match, Notebook notebookToUpdate)
        {
            var alreadyExistingItem = await GetAsync(notebook => notebook.NotebookName == notebookToUpdate.NotebookName && notebook.BucketName == notebookToUpdate.BucketName);
            if (alreadyExistingItem != null)
            {
                notebookToUpdate.Id = alreadyExistingItem.Id;
                await Collection.ReplaceOneAsync(match, notebookToUpdate);
            }
        }

        public async Task<Notebook> GetAsync(Expression<Func<Notebook, bool>> match)
        {
            var notebookWithParametersAndMetadata = await (await Collection.FindAsync(match)).FirstOrDefaultAsync();
            return notebookWithParametersAndMetadata;
        }

        public async Task<IEnumerable<Notebook>> GetAllAsync(Expression<Func<Notebook, bool>> match)
        {
            return await (await Collection.FindAsync(match)).ToListAsync();
        }

        public async Task DeleteAsync(Expression<Func<Notebook, bool>> match)
        {
            await Collection.DeleteManyAsync(match);
        }
    }
}
