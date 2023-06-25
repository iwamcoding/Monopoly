using System.Runtime.Serialization;

namespace BaseMonopoly.Exceptions.TransactionExceptions
{
    [Serializable]
    internal class InsufficientBuildingException : Exception
    {
        public InsufficientBuildingException()
        {
        }

        public InsufficientBuildingException(string? message) : base(message)
        {
        }

        public InsufficientBuildingException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InsufficientBuildingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}