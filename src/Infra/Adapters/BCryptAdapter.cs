using Data.Protocols;

namespace Infra.Adapters
{
    public class BcryptAdapter : IHasher
    {
        public string Generate(string value) => BCrypt.Net.BCrypt.HashPassword(value);
    }
}
