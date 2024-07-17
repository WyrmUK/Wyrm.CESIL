using System;

namespace Wyrm.CESIL.Exceptions
{
    /// <summary>
    /// An exception representing an illegal operation.
    /// </summary>
    public class IllegalOperationException : Exception
    {
        /// <summary>
        /// Creates a new <see cref="IllegalOperationException"/>.
        /// </summary>
        /// <param name="message">The error message.</param>
        public IllegalOperationException(string message) : base(message)
        {
        }
    }
}
