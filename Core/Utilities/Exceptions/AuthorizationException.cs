using System;

namespace Core.Utilities.Exceptions
{
    public class AuthorizationException : Exception
    {
        public AuthorizationException()
        {
        }

        public AuthorizationException(string message)
            : base(message)
        {
        }
        
        public AuthorizationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}