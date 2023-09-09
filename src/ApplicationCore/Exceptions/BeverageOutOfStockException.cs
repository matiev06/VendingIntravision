using System.Runtime.Serialization;

namespace VendingIntravision.ApplicationCore.Exceptions
{
    [Serializable]
    public class BeverageOutOfStockException : Exception
    {
        public BeverageOutOfStockException()
        {
        }

        public BeverageOutOfStockException(string? message) : base(message)
        {
        }

        public BeverageOutOfStockException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected BeverageOutOfStockException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}