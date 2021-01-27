using Infra.Adapters;
using Presentation.Protocols;
using Validations = Utils.Validators;

namespace Factories.Validators
{
    public static class SignUpValidatorBuilder
    {
        public static Validations.CompositeValidation Create()
        {
            var emailValidator = new EmailValidatorAdapter();
            var validators = new IValidator[] {
                new Validations.RequiredFieldValidation(new[] { "Name", "Email", "Password", "PasswordConfirmation" }),
                new Validations.CompareFieldsValidation("Password", "PasswordConfirmation"),
                new Validations.EmailValidation("Email", emailValidator)
            };
            return new Validations.CompositeValidation(validators);
        }
    }
}
