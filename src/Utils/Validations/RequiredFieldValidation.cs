using System.Collections.Generic;
using System.Linq;
using Presentation.Exceptions;
using Presentation.Protocols;

namespace Utils.Validators
{
    public class RequiredFieldValidation : IValidator
    {
        private readonly string[] requiredFields;
        public RequiredFieldValidation(string[] requiredFields) => this.requiredFields = requiredFields;
        public void Validate<T>(T input)
        {
            var invalidFields = new List<string>();
            foreach (var field in this.requiredFields)
            {
                if (!HasField(input, field))
                {
                    invalidFields.Add(field);
                }
            }
            if (invalidFields.Any())
            {
                throw new MissingParameterException(invalidFields);
            }
        }

        private static bool HasField<T>(T request, string field) => request.GetType().GetProperty(field)?.GetValue(request) != null;
    }
}
