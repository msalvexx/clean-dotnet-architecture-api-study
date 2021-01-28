using System;
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

        public override bool Equals(object obj) =>
            obj is AddAccountModel model && this.Name == model.Name && this.Email == model.Email && this.Password == model.Password;

        public override int GetHashCode() =>
            HashCode.Combine(this.Name, this.Email, this.Password);
    }

    public interface IAddAccount
    {
        public Task<IAccount> Add(IAddAccountModel data);
    }
}
