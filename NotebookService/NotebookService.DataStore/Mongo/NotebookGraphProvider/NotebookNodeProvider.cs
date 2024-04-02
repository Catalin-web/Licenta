using MongoDB.Driver;
using NotebookService.Models.Entities.NotebookGraph;
using System.Linq.Expressions;

namespace NotebookService.DataStore.Mongo.NotebookGraphProvider
{
    public class NotebookNodeProvider : INotebookNodeProvider
    {
        private readonly IMongoDataContext _context;
        private readonly string _collectionName = "NotebookGraph";

        public NotebookNodeProvider(IMongoDataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        protected IMongoCollection<NotebookNode> Collection => _context.Database.GetCollection<NotebookNode>(_collectionName);

        public async Task InsertAsync(NotebookNode notebookGraph)
        {
            await Collection.InsertOneAsync(notebookGraph);
        }

        public async Task UpdateAsync(Expression<Func<NotebookNode, bool>> match, NotebookNode notebookGraph)
        {
            await Collection.ReplaceOneAsync(match, notebookGraph);
        }

        public async Task<NotebookNode> GetAsync(Expression<Func<NotebookNode, bool>> match)
        {
            var notebookWithParametersAndMetadata = await (await Collection.FindAsync(match)).FirstOrDefaultAsync();
            return notebookWithParametersAndMetadata;
        }

        public async Task<IEnumerable<NotebookNode>> GetAllAsync(Expression<Func<NotebookNode, bool>> match)
        {
            return await (await Collection.FindAsync(match)).ToListAsync();
        }

        public async Task DeleteAsync(Expression<Func<NotebookNode, bool>> match)
        {
            await Collection.DeleteManyAsync(match);
        }
    }
}
