using MongoDB.Driver;
using NotebookService.Models.Entities.Jobs;
using System.Linq.Expressions;

namespace NotebookService.DataStore.Mongo.Jobs.TriggerNotebookJobsHistoryProvider
{
    public class TriggerNotebookJobsHistoryProvider : ITriggerNotebookJobsHistoryProvider
    {
        private readonly IMongoDataContext _context;
        private readonly string _collectionName = "TriggerNotebookJobsHistory";
        private readonly IMongoCollection<TriggerNotebookJobHistoryModel> Collection;

        protected MongoCollectionSettings CollectionSettings = new MongoCollectionSettings();
        public TriggerNotebookJobsHistoryProvider(IMongoDataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            if (!_context.Database.ListCollectionNames().ToList().Where(name => name == _collectionName).Any())
            {
                _context.Database.CreateCollection(_collectionName, new CreateCollectionOptions() { TimeSeriesOptions = new TimeSeriesOptions("TriggerTime") });
            }
            Collection = _context.Database.GetCollection<TriggerNotebookJobHistoryModel>(_collectionName);
        }

        public async Task InsertAsync(TriggerNotebookJobHistoryModel triggerNotebookJobHistory)
        {
            await Collection.InsertOneAsync(triggerNotebookJobHistory);
        }

        public async Task UpdateAsync(Expression<Func<TriggerNotebookJobHistoryModel, bool>> match, TriggerNotebookJobHistoryModel triggerNotebookJobHistory)
        {
            await Collection.ReplaceOneAsync(match, triggerNotebookJobHistory);
        }

        public async Task<TriggerNotebookJobHistoryModel> GetAsync(Expression<Func<TriggerNotebookJobHistoryModel, bool>> match)
        {
            var notebookWithParametersAndMetadata = await (await Collection.FindAsync(match)).FirstOrDefaultAsync();
            return notebookWithParametersAndMetadata;
        }

        public async Task<IEnumerable<TriggerNotebookJobHistoryModel>> GetAllAsync(Expression<Func<TriggerNotebookJobHistoryModel, bool>> match)
        {
            return await (await Collection.FindAsync(match)).ToListAsync();
        }

        public async Task DeleteAsync(Expression<Func<TriggerNotebookJobHistoryModel, bool>> match)
        {
            await Collection.DeleteManyAsync(match);
        }
    }
}
