using MongoDB.Driver;

namespace Fileservice.DataStore.Mongo
{
    public interface IMongoDataContext
    {
        IMongoDatabase Database { get; }
    }
}
