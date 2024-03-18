using MongoDB.Driver;
using NotebookService.Models.Entities.NotebookGraph;
using System.Linq.Expressions;

namespace NotebookService.DataStore.Mongo.NotebookGraphProvider
{
    public class NotebookGraphProvider : INotebookGraphProvider
    {
        private readonly IMongoDataContext _context;
        private readonly string _collectionName = "NotebookGraph";

        public NotebookGraphProvider(IMongoDataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        protected IMongoCollection<NotebookGraph> Collection => _context.Database.GetCollection<NotebookGraph>(_collectionName);

        public async Task InsertAsync(NotebookGraph notebookGraph)
        {
            await Collection.InsertOneAsync(notebookGraph);
        }

        public async Task UpdateAsync(Expression<Func<NotebookGraph, bool>> match, NotebookGraph notebookGraph)
        {
            await Collection.ReplaceOneAsync(match, notebookGraph);
        }

        public async Task<NotebookGraph> GetAsync(Expression<Func<NotebookGraph, bool>> match)
        {
            var notebookWithParametersAndMetadata = await (await Collection.FindAsync(match)).FirstOrDefaultAsync();
            return notebookWithParametersAndMetadata;
        }

        public async Task<IEnumerable<NotebookGraph>> GetAllAsync(Expression<Func<NotebookGraph, bool>> match)
        {
            return await (await Collection.FindAsync(match)).ToListAsync();
        }

        public async Task DeleteAsync(Expression<Func<NotebookGraph, bool>> match)
        {
            await Collection.DeleteManyAsync(match);
        }
    }
}
