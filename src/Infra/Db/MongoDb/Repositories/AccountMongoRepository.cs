using System.Threading.Tasks;
using Data.Protocols;
using Domain.Models;
using Domain.UseCases;
using Infra.Db.MongoDb.Configurators;

namespace Infra.Db.MongoDb.Repositories
{
    public class AccountMongoRepository : BaseMongoDbRepository, IAddAccountRepository
    {
        public static readonly string CollectionName = "accounts";
        public AccountMongoRepository(MongoDbContext context) : base(context) { }

        public async Task<IAccount> Add(IAddAccountModel data)
        {
            var account = new Account
            {
                Email = data.Email,
                Name = data.Name,
                Password = data.Password
            };
            await this.GetCollection<Account>(CollectionName).InsertOneAsync(account);
            return account;
        }
    }
}
