using System;

namespace Presentation.Exceptions
{
    public class InvalidParameterException : Exception
    {
        public InvalidParameterException(string message) : base($"Invalid Parameter: {message}") { }
    }
}
