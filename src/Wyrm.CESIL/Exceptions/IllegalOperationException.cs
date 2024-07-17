using System;

namespace Wyrm.CESIL.Exceptions
{
    public class IllegalOperationException : Exception
    {
        public IllegalOperationException(string message) : base(message)
        {
        }
    }
}
