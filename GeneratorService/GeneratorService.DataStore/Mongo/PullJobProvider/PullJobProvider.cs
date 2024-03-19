using GeneratorService.DataStore.Mongo.NotebookGraphProvider;
using GeneratorService.Models.Entities.Jobs;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace GeneratorService.DataStore.Mongo.PullJobProvider
{
    public class PullJobProvider : IPullJobProvider
    {
        private readonly IMongoDataContext _context;
        private readonly string _collectionName = "PullJob";

        public PullJobProvider(IMongoDataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        protected IMongoCollection<PullJob> Collection => _context.Database.GetCollection<PullJob>(_collectionName);

        public async Task InsertAsync(PullJob notebookGraph)
        {
            await Collection.InsertOneAsync(notebookGraph);
        }

        public async Task UpdateAsync(Expression<Func<PullJob, bool>> match, PullJob pullJob)
        {
            await Collection.ReplaceOneAsync(match, pullJob);
        }

        public async Task<PullJob> GetAsync(Expression<Func<PullJob, bool>> match)
        {
            var notebookWithParametersAndMetadata = await (await Collection.FindAsync(match)).FirstOrDefaultAsync();
            return notebookWithParametersAndMetadata;
        }

        public async Task<IEnumerable<PullJob>> GetAllAsync(Expression<Func<PullJob, bool>> match)
        {
            return await (await Collection.FindAsync(match)).ToListAsync();
        }

        public async Task DeleteAsync(Expression<Func<PullJob, bool>> match)
        {
            await Collection.DeleteManyAsync(match);
        }
    }
}
