using Presentation.Exceptions;
using Presentation.Protocols;
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
            if (this.IsMissingProperty(input))
            {
                throw new InvalidParameterException(this.fieldName);
            }
            var fieldToValidate = this.GetFieldToValidate(input);
            if (!this.emailValidator.IsValid(fieldToValidate))
            {
                throw new InvalidParameterException(this.fieldName);
            }
        }

        private string GetFieldToValidate<T>(T input) => input.GetType().GetProperty(this.fieldName).GetValue(input).ToString();
        private bool IsMissingProperty<T>(T input) => input.GetType().GetProperty(this.fieldName) is null;
    }
}
