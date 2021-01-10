using System.Collections.Generic;
using Presentation.Exceptions;
using Presentation.Protocols;

namespace Utils
{
    public class ValidatorAdapter : IValidator
    {
        public void AssertParameterIsEqual(object first, object second, string parameterNameToThrowOnError)
        {
            if (!first.Equals(second))
            {
                throw new InvalidParameterException(parameterNameToThrowOnError);
            }
        }
        public void AssertHasRequiredFields<T>(T request, string[] requiredFields)
        {
            var hasInvalidFields = false;
            var invalidFields = new List<string>();
            foreach (var field in requiredFields)
            {
                var hasField = request.GetType().GetProperty(field)?.GetValue(request) != null;
                if (!hasField)
                {
                    hasInvalidFields = true;
                    invalidFields.Add(field);
                }
            }
            if (hasInvalidFields)
            {
                throw new MissingParameterException(invalidFields);
            }
        }
    }
}
