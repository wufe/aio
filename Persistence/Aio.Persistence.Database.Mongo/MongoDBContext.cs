using System;
using System.Linq;
using Aio.Persistence.Database.Mongo.Configuration.Interface;
using MongoDB.Driver;

namespace Aio.Persistence.Database.Mongo {
    public class MongoDBContext {
        private readonly IDatabaseSettings _databaseSettings;
        private readonly IMongoDatabase _database;

        public MongoDBContext(IDatabaseSettings databaseSettings)
        {
            _databaseSettings = databaseSettings;
            var client = new MongoClient(databaseSettings.ConnectionString);
            _database = client.GetDatabase(databaseSettings.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>() {
            var collectionName = ResolveCollectionNameFromType<T>();
            if (collectionName == null)
                throw new ArgumentException($"Could not find a collection name for type ${typeof(T).Name}.");
            return _database.GetCollection<T>(collectionName);
        }

        private string ResolveCollectionNameFromType<T>() {
            var name = typeof(T).Name;
            _databaseSettings.CollectionNames
                .TryGetValue(name, out var collectionName);
            return collectionName;
        }

    }
}