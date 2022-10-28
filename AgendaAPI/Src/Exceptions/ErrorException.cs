using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaAPI.Src.Exceptions
{
    public class ErrorException : Exception
    {
        public int StatusCode = 400;

        public ErrorException()
        {
        }

        public ErrorException(string message)
            : base(message)
        {
        }

        public ErrorException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
