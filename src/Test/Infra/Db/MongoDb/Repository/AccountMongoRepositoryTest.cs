using System.Threading.Tasks;
using Domain.Models;
using Domain.UseCases;
using FluentAssertions;
using Infra.Db.MongoDb.Configurators;
using Infra.Db.MongoDb.Models;
using Infra.Db.MongoDb.Repositories;
using MongoDB.Driver;
using Xunit;

namespace Infra.Db.MongoDb
{
    public class AccountMongoRepositoryTest
    {
        private readonly IMongoCollection<IAccount> accountCollection;
        private readonly MongoDbContext context;

        public AccountMongoRepositoryTest()
        {
            var mongoSettings = new MongoDbSettings();
            this.context = new MongoDbContext(mongoSettings);
            this.accountCollection = this.context.GetCollection<IAccount>("accounts");
            this.accountCollection.DeleteMany(Builders<IAccount>.Filter.Empty);
        }

        public AccountMongoRepository MakeSut() => new(this.context);
        public static IAddAccountModel MakeFakeAccountModel() => new AddAccountModel
        {
            Name = "any_name",
            Email = "any_email@mail.com",
            Password = "hashed_password"
        };

        [Fact]
        public async Task ShouldReturnAnAccountOnSuccess()
        {
            var sut = this.MakeSut();
            var fakeData = MakeFakeAccountModel();
            var account = await sut.Add(fakeData);
            account.Should().BeEquivalentTo(fakeData);
        }
    }
}
