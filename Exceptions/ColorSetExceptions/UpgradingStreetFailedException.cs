using System.Runtime.Serialization;

namespace BaseMonopoly.Exceptions.ColorSetExceptions
{
    [Serializable]
    internal class UpgradingStreetFailedException : Exception
    {
        public UpgradingStreetFailedException()
        {
        }

        public UpgradingStreetFailedException(string? message) : base(message)
        {
        }

        public UpgradingStreetFailedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UpgradingStreetFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}