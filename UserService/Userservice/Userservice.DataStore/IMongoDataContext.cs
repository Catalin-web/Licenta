using MongoDB.Driver;

namespace Userservice.DataStore
{
    public interface IMongoDataContext
    {
        IMongoDatabase Database { get; }
    }
}
