using System.Threading.Tasks;
using Domain.Models;

namespace Domain.UseCases
{
    public interface IAddAccountModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AddAccountModel : IAddAccountModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public interface IAddAccount
    {
        public Task<IAccount> Add(IAddAccountModel data);
    }
}
