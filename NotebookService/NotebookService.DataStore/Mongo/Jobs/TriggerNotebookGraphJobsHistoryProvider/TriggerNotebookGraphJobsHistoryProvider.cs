using MongoDB.Driver;
using NotebookService.Models.Entities.Jobs;
using System.Linq.Expressions;

namespace NotebookService.DataStore.Mongo.Jobs.TriggerNotebookGraphJobsHistoryProvider
{
    public class TriggerNotebookGraphJobsHistoryProvider : ITriggerNotebookGraphJobsHistoryProvider
    {
        private readonly IMongoDataContext _context;
        private readonly string _collectionName = "TriggerNotebookGraphJobsHistory";
        private readonly IMongoCollection<TriggerNotebookGraphJobHistoryModel> Collection;

        protected MongoCollectionSettings CollectionSettings = new MongoCollectionSettings();
        public TriggerNotebookGraphJobsHistoryProvider(IMongoDataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            if (!_context.Database.ListCollectionNames().ToList().Where(name => name == _collectionName).Any())
            {
                _context.Database.CreateCollection(_collectionName, new CreateCollectionOptions() { TimeSeriesOptions = new TimeSeriesOptions("TriggerTime") });
            }
            Collection = _context.Database.GetCollection<TriggerNotebookGraphJobHistoryModel>(_collectionName);
        }

        public async Task InsertAsync(TriggerNotebookGraphJobHistoryModel triggerNotebookJobHistory)
        {
            await Collection.InsertOneAsync(triggerNotebookJobHistory);
        }

        public async Task UpdateAsync(Expression<Func<TriggerNotebookGraphJobHistoryModel, bool>> match, TriggerNotebookGraphJobHistoryModel triggerNotebookJobHistory)
        {
            await Collection.ReplaceOneAsync(match, triggerNotebookJobHistory);
        }

        public async Task<TriggerNotebookGraphJobHistoryModel> GetAsync(Expression<Func<TriggerNotebookGraphJobHistoryModel, bool>> match)
        {
            var notebookWithParametersAndMetadata = await (await Collection.FindAsync(match)).FirstOrDefaultAsync();
            return notebookWithParametersAndMetadata;
        }

        public async Task<IEnumerable<TriggerNotebookGraphJobHistoryModel>> GetAllAsync(Expression<Func<TriggerNotebookGraphJobHistoryModel, bool>> match)
        {
            return await (await Collection.FindAsync(match)).ToListAsync();
        }

        public async Task DeleteAsync(Expression<Func<TriggerNotebookGraphJobHistoryModel, bool>> match)
        {
            await Collection.DeleteManyAsync(match);
        }
    }
}
