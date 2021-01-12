using System.Collections.Generic;
using System.Linq;
using Presentation.Exceptions;
using Presentation.Protocols;

namespace Utils
{
    public class ValidatorAdapter : IValidator
    {
        public void ParameterIsEqual(object first, object second, string parameterNameToThrowOnError)
        {
            if (!first.Equals(second))
            {
                throw new InvalidParameterException(parameterNameToThrowOnError);
            }
        }

        public void HasRequiredFields<T>(T request, string[] requiredFields)
        {
            var invalidFields = new List<string>();
            foreach (var field in requiredFields)
            {
                if (!HasField(request, field))
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

        public void IsValidEmail(string value, string parameterNameToThrowOnError)
        {
        }
    }
}
