using MongoDB.Driver;

namespace NotebookService.DataStore.Mongo
{
    public interface IMongoDataContext
    {
        IMongoDatabase Database { get; }
    }
}
