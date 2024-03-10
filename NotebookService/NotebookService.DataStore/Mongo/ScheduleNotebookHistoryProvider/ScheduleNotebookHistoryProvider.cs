using MongoDB.Driver;
using NotebookService.Models.Entities.ScheduleNotebook;
using System.Linq.Expressions;

namespace NotebookService.DataStore.Mongo.ScheduleNotebookHistoryProvider
{
    public class ScheduleNotebookHistoryProvider : IScheduleNotebookHistoryProvider
    {
        private readonly IMongoDataContext _context;
        private readonly string _collectionName = "ScheduledNotebookHistory";
        private readonly IMongoCollection<ScheduledNotebook> Collection;

        protected MongoCollectionSettings CollectionSettings = new MongoCollectionSettings();
        public ScheduleNotebookHistoryProvider(IMongoDataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            if (!_context.Database.ListCollectionNames().ToList().Where(name => name == _collectionName).Any())
            {
                _context.Database.CreateCollection(_collectionName, new CreateCollectionOptions() { TimeSeriesOptions = new TimeSeriesOptions("FinishedAt") });
            }
            Collection = _context.Database.GetCollection<ScheduledNotebook>(_collectionName);
        }

        public async Task InsertAsync(ScheduledNotebook scheduledNotebook)
        {
            await Collection.InsertOneAsync(scheduledNotebook);
        }

        public async Task UpdateAsync(Expression<Func<ScheduledNotebook, bool>> match, ScheduledNotebook scheduledNotebook)
        {
            await Collection.ReplaceOneAsync(match, scheduledNotebook);
        }

        public async Task<ScheduledNotebook> GetAsync(Expression<Func<ScheduledNotebook, bool>> match)
        {
            var notebookWithParametersAndMetadata = await (await Collection.FindAsync(match)).FirstOrDefaultAsync();
            return notebookWithParametersAndMetadata;
        }

        public async Task<IEnumerable<ScheduledNotebook>> GetAllAsync(Expression<Func<ScheduledNotebook, bool>> match)
        {
            return await (await Collection.FindAsync(match)).ToListAsync();
        }

        public async Task DeleteAsync(Expression<Func<ScheduledNotebook, bool>> match)
        {
            await Collection.DeleteManyAsync(match);
        }
    }
}
