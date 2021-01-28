using System.Threading.Tasks;
using Data.Protocols;
using Domain.Models;
using Domain.UseCases;

namespace Data.UseCases
{
    public class DbAddAccount : IAddAccount
    {
        private readonly IHasher hasher;
        private readonly IAddAccountRepository repo;

        public DbAddAccount(IHasher hasher, IAddAccountRepository repo)
        {
            this.hasher = hasher;
            this.repo = repo;
        }

        public Task<IAccount> Add(IAddAccountModel data)
        {
            var hashedPassword = this.hasher.Generate(data.Password);
            this.repo.Add(new AddAccountModel
            {
                Name = data.Name,
                Email = data.Email,
                Password = hashedPassword
            });
            return null;
        }
    }
}
