using Presentation.Exceptions;
using Utils.Protocols;

namespace Utils.Validators
{
    public class CompareFieldsValidation : IValidator
    {
        private readonly string fieldName;
        private readonly string fieldNameToCompare;
        public CompareFieldsValidation(string fieldName, string fieldNameToCompare)
        {
            this.fieldName = fieldName;
            this.fieldNameToCompare = fieldNameToCompare;
        }
        public void Validate<T>(T input)
        {
            var fieldNameValue = this.GetFieldValue(input, this.fieldName);
            var fieldNameToCompareValue = this.GetFieldValue(input, this.fieldNameToCompare);
            if (!fieldNameValue.Equals(fieldNameToCompareValue))
            {
                throw new InvalidParameterException(this.fieldNameToCompare);
            }
        }

        private object GetFieldValue<T>(T input, string field)
        {
            try
            {
                return input.GetType().GetProperty(field).GetValue(input);
            }
            catch
            {
                throw new InvalidParameterException(this.fieldNameToCompare);
            }
        }
    }
}
