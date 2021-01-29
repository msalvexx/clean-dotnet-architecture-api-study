namespace Presentation.Exceptions
{
    public class InvalidParameterException : ValidationException
    {
        public InvalidParameterException(string message) : base($"Invalid Parameter: {message}") { }
    }
}
