using Presentation.Exceptions;
using Utils.Protocols;

namespace Utils.Validators
{
    public class EmailValidation : IValidator
    {
        private readonly string fieldName;
        public EmailValidation(string fieldName)
        {
            this.fieldName = fieldName;
        }

        public void Validate<T>(T input)
        {
            throw new InvalidParameterException(this.fieldName);
        }
    }
}
