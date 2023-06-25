using System.Runtime.Serialization;

namespace BaseMonopoly.Exceptions.ColorSetExceptions
{
    [Serializable]
    internal class DowngradingStreetFailedException : Exception
    {
        public DowngradingStreetFailedException()
        {
        }

        public DowngradingStreetFailedException(string? message) : base(message)
        {
        }

        public DowngradingStreetFailedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DowngradingStreetFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}