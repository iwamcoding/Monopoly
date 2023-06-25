using System.Runtime.Serialization;

namespace BaseMonopoly.Exceptions.TransactionExceptions
{
    [Serializable]
    internal class BidFailedException : Exception
    {
        public BidFailedException()
        {
        }

        public BidFailedException(string? message) : base(message)
        {
        }

        public BidFailedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected BidFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}