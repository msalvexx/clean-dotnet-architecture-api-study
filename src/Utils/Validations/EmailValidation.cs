using Presentation.Exceptions;
using Utils.Protocols;

namespace Utils.Validators
{
    public class EmailValidation : IValidator
    {
        private readonly string fieldName;
        private readonly IEmailValidator emailValidator;
        public EmailValidation(string fieldName, IEmailValidator emailValidator)
        {
            this.fieldName = fieldName;
            this.emailValidator = emailValidator;
        }

        public void Validate<T>(T input)
        {
            var fieldToValidate = input.GetType().GetProperty(this.fieldName)?.GetValue(input).ToString();
            if (!this.emailValidator.IsValid(fieldToValidate))
            {
                throw new InvalidParameterException(this.fieldName);
            }
        }
    }
}
