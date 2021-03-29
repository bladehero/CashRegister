using System;
using System.Runtime.Serialization;

namespace CashRegister.Models.Exceptions.SessionExceptions
{
    public class MultipleSessionOpenedException : Exception
    {
        public MultipleSessionOpenedException()
        {
        }

        public MultipleSessionOpenedException(string message) : base(message)
        {
        }

        public MultipleSessionOpenedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MultipleSessionOpenedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}