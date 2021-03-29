using System;
using System.Runtime.Serialization;

namespace CashRegister.Models.Exceptions.UserStorageExceptions
{
    public class NoUserDefinedException : Exception
    {
        public NoUserDefinedException()
        {
        }

        public NoUserDefinedException(string message) : base(message)
        {
        }

        public NoUserDefinedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoUserDefinedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}