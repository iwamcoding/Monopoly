namespace BaseMonopoly.Exceptions.TransactionExceptions
{
    public class TransactionInvalidStateException : Exception
    {
        public TransactionInvalidStateException() { }
        public TransactionInvalidStateException(string message) : base(message) { }
        public TransactionInvalidStateException(string message, Exception innerException) : base(message, innerException) { }
    }
}
