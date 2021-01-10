using System;

namespace Presentation.Exceptions
{
    public class ServerErrorException : Exception
    {
        public ServerErrorException() : base($"An internal error was occurred") { }
    }
}
