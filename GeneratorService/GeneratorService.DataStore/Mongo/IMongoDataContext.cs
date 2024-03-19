using MongoDB.Driver;

namespace GeneratorService.DataStore.Mongo
{
    public interface IMongoDataContext
    {
        IMongoDatabase Database { get; }
    }
}
