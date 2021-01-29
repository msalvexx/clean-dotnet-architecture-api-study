using Infra.Db.MongoDb.Configurators;
using MongoDB.Driver;

namespace Infra.Db.MongoDb.Repositories
{
    public interface IBaseMongoDbRepository
    {
        IMongoCollection<TEntity> GetCollection<TEntity>(string collectionName) where TEntity : class;
    }

    public class BaseMongoDbRepository : IBaseMongoDbRepository
    {
        private readonly MongoDbContext context;
        public BaseMongoDbRepository(MongoDbContext context)
            => this.context = context;

        public IMongoCollection<TEntity> GetCollection<TEntity>(string collectionName) where TEntity : class
            => this.context.GetCollection<TEntity>(collectionName);
    }
}
