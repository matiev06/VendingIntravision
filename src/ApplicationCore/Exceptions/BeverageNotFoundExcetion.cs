using System.Runtime.Serialization;

namespace VendingIntravision.ApplicationCore.Exceptions
{
    [Serializable]
    public class BeverageNotFoundExcetion : Exception
    {
        private int idbeverage;

        public BeverageNotFoundExcetion()
        {
        }

        public BeverageNotFoundExcetion(int idbeverage)
        {
            this.idbeverage = idbeverage;
        }

        public BeverageNotFoundExcetion(string? message) : base(message)
        {
        }

        public BeverageNotFoundExcetion(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected BeverageNotFoundExcetion(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}