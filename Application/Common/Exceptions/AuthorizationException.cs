using System;
using System.Collections.Generic;
using System.Text;

namespace GhostWriter.Application.Common.Exceptions
{
    public class AuthorizationException : Exception
    {
        public AuthorizationException()
           : base()
        {
        }

        public AuthorizationException(string message)
            : base($"Authorization Error: {message}")
        {
        }

        public AuthorizationException(string message, Exception innerException)
            : base($"Authorization Error: {message}", innerException)
        {
        }
    }
}
