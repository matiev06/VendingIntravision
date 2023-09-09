using System.Runtime.Serialization;

namespace VendingIntravision.Web.Exceptions
{
    [Serializable]
    internal class EmptyDataEx : Exception
    {
        public EmptyDataEx()
        {
        }

        public EmptyDataEx(string? message) : base(message)
        {
        }

        public EmptyDataEx(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected EmptyDataEx(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}