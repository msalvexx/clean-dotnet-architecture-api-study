using Infra.Db.MongoDb.Mappers;

namespace Infra.Db.MongoDb.Configurators
{
    public static class MongoDbMapper
    {
        public static void Map() => AccountMapper.Map();
    }
}
