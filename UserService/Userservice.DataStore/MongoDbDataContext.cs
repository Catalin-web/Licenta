using MongoDB.Driver;

namespace Userservice.DataStore
{
    public class MongoDataContext : IMongoDataContext
    {
        private readonly MongoUrl _mongoUrl;
        private readonly Lazy<IMongoDatabase> _database;

        public IMongoDatabase Database => _database.Value;

        public MongoDataContext(string connectionString)
        {
            _mongoUrl = new MongoUrl(connectionString);
            _database = new Lazy<IMongoDatabase>(CreateDatabaseConnection);
        }

        private IMongoDatabase CreateDatabaseConnection()
        {
            var client = new MongoClient(_mongoUrl);
            var database = client.GetDatabase(_mongoUrl.DatabaseName);
            return database;
        }
    }
}
