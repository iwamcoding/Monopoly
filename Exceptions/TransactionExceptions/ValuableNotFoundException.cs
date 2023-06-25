using System.Runtime.Serialization;

namespace BaseMonopoly.Exceptions.TransactionExceptions
{
    [Serializable]
    internal class ValuableNotFoundException : Exception
    {
        public ValuableNotFoundException()
        {
        }

        public ValuableNotFoundException(string? message) : base(message)
        {
        }

        public ValuableNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ValuableNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}