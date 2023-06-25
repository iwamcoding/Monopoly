using System.Runtime.Serialization;

namespace BaseMonopoly.Exceptions.TitleDeedExceptions
{
    [Serializable]
    internal class TitleDeedCannotBeMortgagedException : Exception
    {
        public TitleDeedCannotBeMortgagedException()
        {
        }

        public TitleDeedCannotBeMortgagedException(string? message) : base(message)
        {
        }

        public TitleDeedCannotBeMortgagedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected TitleDeedCannotBeMortgagedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}