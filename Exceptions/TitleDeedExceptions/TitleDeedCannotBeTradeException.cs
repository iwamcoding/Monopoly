using System.Runtime.Serialization;

namespace BaseMonopoly.Exceptions.TitleDeedExceptions
{
    [Serializable]
    internal class TitleDeedCannotBeTradeException : Exception
    {
        public TitleDeedCannotBeTradeException()
        {
        }

        public TitleDeedCannotBeTradeException(string? message) : base(message)
        {
        }

        public TitleDeedCannotBeTradeException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected TitleDeedCannotBeTradeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}