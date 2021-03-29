using System;
using System.Runtime.Serialization;

namespace CashRegister.Models.Exceptions.SessionExceptions
{
    public class ClosedSessionException : Exception
    {
        public ClosedSessionException()
        {
        }

        public ClosedSessionException(string message) : base(message)
        {
        }

        public ClosedSessionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ClosedSessionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}