using System;
using System.Collections.Generic;

namespace Presentation.Exceptions
{
    public class MissingParameterException : Exception
    {
        public MissingParameterException(string message) : base($"Missing Parameter: {message}") { }
        public MissingParameterException(IEnumerable<string> messages) : base("Missing Parameters: " + string.Join(", ", messages)) { }
    }
}
