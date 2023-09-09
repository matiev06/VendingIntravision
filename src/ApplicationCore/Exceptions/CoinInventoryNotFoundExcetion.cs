using System.Runtime.Serialization;

namespace VendingIntravision.ApplicationCore.Exceptions
{
    [Serializable]
    public class CoinInventoryNotFoundExcetion : Exception
    {
        private int idbeverage;

        public CoinInventoryNotFoundExcetion()
        {
        }

        public CoinInventoryNotFoundExcetion(int idbeverage)
        {
            this.idbeverage = idbeverage;
        }

        public CoinInventoryNotFoundExcetion(string? message) : base(message)
        {
        }

        public CoinInventoryNotFoundExcetion(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected CoinInventoryNotFoundExcetion(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}