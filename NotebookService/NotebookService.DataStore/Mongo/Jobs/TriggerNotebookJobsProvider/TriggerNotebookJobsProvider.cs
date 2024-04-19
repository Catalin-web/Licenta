using MongoDB.Driver;
using NotebookService.Models.Entities.Jobs;
using System.Linq.Expressions;

namespace NotebookService.DataStore.Mongo.Jobs.TriggerNotebookJobsProvider
{
    public class TriggerNotebookJobsProvider : ITriggerNotebookJobsProvider
    {
        private readonly IMongoDataContext _context;
        private readonly string _collectionName = "TriggerNotebookJobs";

        public TriggerNotebookJobsProvider(IMongoDataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        protected IMongoCollection<TriggerNotebookJobModel> Collection => _context.Database.GetCollection<TriggerNotebookJobModel>(_collectionName);

        public async Task InsertAsync(TriggerNotebookJobModel triggerNotebookJobModel)
        {
            await Collection.InsertOneAsync(triggerNotebookJobModel);
        }

        public async Task UpdateAsync(Expression<Func<TriggerNotebookJobModel, bool>> match, TriggerNotebookJobModel triggerNotebookJobModel)
        {
            await Collection.ReplaceOneAsync(match, triggerNotebookJobModel);
        }

        public async Task<TriggerNotebookJobModel> GetAsync(Expression<Func<TriggerNotebookJobModel, bool>> match)
        {
            var notebookWithParametersAndMetadata = await (await Collection.FindAsync(match)).FirstOrDefaultAsync();
            return notebookWithParametersAndMetadata;
        }

        public async Task<IEnumerable<TriggerNotebookJobModel>> GetAllAsync(Expression<Func<TriggerNotebookJobModel, bool>> match)
        {
            return await (await Collection.FindAsync(match)).ToListAsync();
        }

        public async Task DeleteAsync(Expression<Func<TriggerNotebookJobModel, bool>> match)
        {
            await Collection.DeleteManyAsync(match);
        }
    }
}
