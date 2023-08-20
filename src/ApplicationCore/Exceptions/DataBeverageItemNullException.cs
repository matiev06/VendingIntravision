using System.Runtime.Serialization;

namespace VendingIntravision.ApplicationCore.Exceptions
{
    [Serializable]
    internal class DataBeverageItemNullException : Exception
    {
        public DataBeverageItemNullException()
        {
        }

        public DataBeverageItemNullException(string? message) : base(message)
        {
        }

        public DataBeverageItemNullException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DataBeverageItemNullException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}