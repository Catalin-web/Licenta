using MongoDB.Driver;
using NotebookService.Models.Entities.ScheduleNotebook;
using System.Linq.Expressions;

namespace NotebookService.DataStore.Mongo.ScheduleNotebookProvider
{
    public class ScheduleNotebookProvider : IScheduleNotebookProvider
    {
        private readonly IMongoDataContext _context;
        private readonly string _collectionName = "ScheduledNotebook";

        public ScheduleNotebookProvider(IMongoDataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        protected IMongoCollection<ScheduledNotebook> Collection => _context.Database.GetCollection<ScheduledNotebook>(_collectionName);

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
