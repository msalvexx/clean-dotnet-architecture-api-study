using System.Threading.Tasks;
using Domain.Models;
using Domain.UseCases;

namespace Data.Protocols
{
    public interface IAddAccountRepository
    {
        Task<IAccount> Add(IAddAccountModel data);
    }
}
