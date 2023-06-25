using System.Runtime.Serialization;

namespace BaseMonopoly.Exceptions.PlayerExceptions
{
    [Serializable]
    internal class PlayerInJailException : Exception
    {
        public PlayerInJailException()
        {
        }

        public PlayerInJailException(string? message) : base(message)
        {
        }

        public PlayerInJailException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected PlayerInJailException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}