using MongoDB.Driver;
using NotebookService.Models.Entities.Jobs;
using System.Linq.Expressions;

namespace NotebookService.DataStore.Mongo.Jobs.TriggerNotebookGraphJobsProvider
{
    public class TriggerNotebookGraphJobsProvider : ITriggerNotebookGraphJobsProvider
    {
        private readonly IMongoDataContext _context;
        private readonly string _collectionName = "TriggerNotebookGraphJobs";

        public TriggerNotebookGraphJobsProvider(IMongoDataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        protected IMongoCollection<TriggerNotebookGraphJobModel> Collection => _context.Database.GetCollection<TriggerNotebookGraphJobModel>(_collectionName);

        public async Task InsertAsync(TriggerNotebookGraphJobModel triggerNotebookJobModel)
        {
            await Collection.InsertOneAsync(triggerNotebookJobModel);
        }

        public async Task UpdateAsync(Expression<Func<TriggerNotebookGraphJobModel, bool>> match, TriggerNotebookGraphJobModel triggerNotebookJobModel)
        {
            await Collection.ReplaceOneAsync(match, triggerNotebookJobModel);
        }

        public async Task<TriggerNotebookGraphJobModel> GetAsync(Expression<Func<TriggerNotebookGraphJobModel, bool>> match)
        {
            var notebookWithParametersAndMetadata = await (await Collection.FindAsync(match)).FirstOrDefaultAsync();
            return notebookWithParametersAndMetadata;
        }

        public async Task<IEnumerable<TriggerNotebookGraphJobModel>> GetAllAsync(Expression<Func<TriggerNotebookGraphJobModel, bool>> match)
        {
            return await (await Collection.FindAsync(match)).ToListAsync();
        }

        public async Task DeleteAsync(Expression<Func<TriggerNotebookGraphJobModel, bool>> match)
        {
            await Collection.DeleteManyAsync(match);
        }
    }
}
