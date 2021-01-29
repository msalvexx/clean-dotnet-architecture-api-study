using Domain.Models;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Infra.Db.MongoDb.Mappers
{
    public class AccountMapper
    {
        public static void Map() =>
            BsonClassMap.RegisterClassMap<IAccount>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapIdMember(x => x.Id).SetIdGenerator(ObjectIdGenerator.Instance);
                map.MapMember(x => x.Name).SetIsRequired(true);
                map.MapMember(x => x.Email).SetIsRequired(false);
                map.MapMember(x => x.Password).SetIsRequired(true);
            });
    }
}
