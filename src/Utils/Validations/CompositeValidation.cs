using Presentation.Protocols;

namespace Utils.Validators
{
    public class CompositeValidation : IValidator
    {
        private readonly IValidator[] validators;

        public CompositeValidation(IValidator[] validators) => this.validators = validators;

        public void Validate<T>(T input)
        {
            foreach (var validator in this.validators)
            {
                validator.Validate(input);
            }
        }
    }
}
