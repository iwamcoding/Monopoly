using System.Runtime.Serialization;

namespace BaseMonopoly.Exceptions.TransactionExceptions
{
    [Serializable]
    internal class TransactionInvalidTypeException : Exception
    {
        public TransactionInvalidTypeException()
        {
        }

        public TransactionInvalidTypeException(string? message) : base(message)
        {
        }

        public TransactionInvalidTypeException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected TransactionInvalidTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}