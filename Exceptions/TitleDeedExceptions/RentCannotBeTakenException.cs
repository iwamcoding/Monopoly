using System.Runtime.Serialization;

namespace BaseMonopoly.Exceptions.TitleDeedExceptions
{
    [Serializable]
    internal class RentCannotBeTakenException : Exception
    {
        public RentCannotBeTakenException()
        {
        }

        public RentCannotBeTakenException(string? message) : base(message)
        {
        }

        public RentCannotBeTakenException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected RentCannotBeTakenException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}