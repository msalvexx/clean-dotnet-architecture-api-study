using System.Threading.Tasks;
using Data.Protocols;
using Domain.Models;
using Domain.UseCases;

namespace Data.UseCases
{
    public class DbAddAccount : IAddAccount
    {
        private readonly IHasher hasher;

        public DbAddAccount(IHasher hasher) => this.hasher = hasher;

        public Task<IAccount> Add(IAddAccountModel data)
        {
            this.hasher.Generate(data.Password);
            return null;
        }
    }
}
