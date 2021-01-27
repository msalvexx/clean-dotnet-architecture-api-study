using EmailValidation;
using Utils.Protocols;

namespace Infra.Adapters
{
    public class EmailValidatorAdapter : IEmailValidator
    {
        public bool IsValid(string email) => EmailValidator.Validate(email);
    }
}
