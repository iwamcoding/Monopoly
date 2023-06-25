using System.Runtime.Serialization;

namespace BaseMonopoly.Exceptions.TransactionExceptions
{
    [Serializable]
    internal class ValuableCannotBeAddedException : Exception
    {
        public ValuableCannotBeAddedException()
        {
        }

        public ValuableCannotBeAddedException(string? message) : base(message)
        {
        }

        public ValuableCannotBeAddedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ValuableCannotBeAddedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}