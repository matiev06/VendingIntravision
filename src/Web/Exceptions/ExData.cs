using System.Runtime.Serialization;

namespace VendingIntravision.Web.Exceptions
{
    [Serializable]
    internal class ExData : Exception
    {
        public ExData()
        {
        }

        public ExData(string? message) : base(message)
        {
        }

        public ExData(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ExData(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}