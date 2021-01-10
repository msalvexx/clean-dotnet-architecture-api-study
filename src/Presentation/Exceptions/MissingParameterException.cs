using System;
using System.Collections.Generic;

namespace Presentation.Exceptions
{
    public class MissingParameterException : Exception
    {
        public MissingParameterException(string message) : base(message) { }
        public MissingParameterException(IEnumerable<string> messages) : base(string.Join(",", messages)) { }
    }
}
