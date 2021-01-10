namespace Presentation.Protocols
{
    public interface IValidator
    {
        /// <summary>
        /// Check if request contain required fields
        /// </summary>
        /// <param name="request">Request</param>
        /// <param name="requiredFields">The string array containing all required parameters</param>
        /// <typeparam name="T">The request type</typeparam>
        /// <exception cref="Exceptions.MissingParameterException"></exception>
        /// <returns></returns>
        public void AssertHasRequiredFields<T>(T request, string[] requiredFields);

        /// <summary>
        /// Check if objects are equal
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public void AssertParameterIsEqual(object first, object second, string parameterNameToThrowOnError);
    }
}
