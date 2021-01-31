namespace Infra.Db.MongoDb.Models
{
    public interface IMongoDbSettings
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
    }

    public class MongoDbSettings : IMongoDbSettings
    {
        public string DatabaseName { get; set; } = "clean-dotnet-api";
        public string ConnectionString { get; set; } = "mongodb://mongo:27017/clean-node-api";
    }
}
