using Presentation.Exceptions;
using Utils.Protocols;

namespace Utils.Validators
{
    public class CompareFieldsValidator : IValidator
    {
        private readonly string fieldName;
        private readonly string fieldNameToCompare;
        public CompareFieldsValidator(string fieldName, string fieldNameToCompare)
        {
            this.fieldName = fieldName;
            this.fieldNameToCompare = fieldNameToCompare;
        }
        public void Validate<T>(T input)
        {
            var fieldNameValue = GetFieldNameValue(input);
            var fieldNameToCompareValue = GetFieldNameToCompareValue(input);
            if (fieldNameValue == null || fieldNameToCompareValue == null || !fieldNameValue.Equals(fieldNameToCompareValue))
            {
                throw new InvalidParameterException(this.fieldNameToCompare);
            }
        }

        private object GetFieldNameValue<T>(T input) => input?.GetType()?.GetProperty(this.fieldName)?.GetValue(input);

        private object GetFieldNameToCompareValue<T>(T input) => input?.GetType()?.GetProperty(this.fieldNameToCompare)?.GetValue(input);
    }
}
