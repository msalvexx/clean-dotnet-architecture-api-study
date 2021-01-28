using System;

namespace Domain.Models
{
    public interface IAccount
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class Account : IAccount
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public override bool Equals(object obj) => obj is Account account && this.Id == account.Id && this.Name == account.Name && this.Email == account.Email && this.Password == account.Password;
        public override int GetHashCode() => HashCode.Combine(this.Id, this.Name, this.Email, this.Password);
    }
}
