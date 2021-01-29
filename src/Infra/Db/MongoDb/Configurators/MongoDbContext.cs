using Infra.Db.MongoDb.Models;
using MongoDB.Driver;

namespace Infra.Db.MongoDb.Configurators
{
    public class MongoDbContext
    {
        private readonly IMongoDbSettings settings;
        private readonly MongoClient client;

        public MongoDbContext(IMongoDbSettings settings)
        {
            this.settings = settings;
            this.client = new MongoClient(this.settings.ConnectionString);
        }

        private IMongoDatabase GetDatabase()
            => this.client.GetDatabase(this.settings.DatabaseName);

        public IMongoCollection<TEntity> GetCollection<TEntity>(string collectionName) where TEntity : class
            => this.GetDatabase().GetCollection<TEntity>(collectionName);

    }
}
