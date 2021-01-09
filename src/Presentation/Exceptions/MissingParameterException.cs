using System;

namespace Presentation.Exceptions
{
    public class MissingParameterException : Exception
    {
        public MissingParameterException(string message) : base(message) { }
    }
}
