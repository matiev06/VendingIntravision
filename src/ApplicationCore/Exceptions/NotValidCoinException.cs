using System.Runtime.Serialization;

namespace VendingIntravision.ApplicationCore.Exceptions
{
    [Serializable]
    public class NotValidCoinException : Exception
    {
        public NotValidCoinException()
        {
        }

        public NotValidCoinException(string? message) : base(message)
        {
        }

        public NotValidCoinException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NotValidCoinException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}