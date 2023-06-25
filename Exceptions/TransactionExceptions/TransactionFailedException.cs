using System.Runtime.Serialization;

namespace BaseMonopoly.Exceptions.TransactionExceptions
{
    [Serializable]
    internal class TransactionFailedException : Exception
    {
        private string v;
        private InsufficientMoneyException insufficientMoneyException;

        public TransactionFailedException()
        {
        }

        public TransactionFailedException(string? message) : base(message)
        {
        }

        public TransactionFailedException(string v, InsufficientMoneyException insufficientMoneyException)
        {
            this.v = v;
            this.insufficientMoneyException = insufficientMoneyException;
        }

        public TransactionFailedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected TransactionFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}