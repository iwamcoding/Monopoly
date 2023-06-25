using System.Runtime.Serialization;

namespace BaseMonopoly.Exceptions.PlayerExceptions
{
    [Serializable]
    internal class PlayerEndTurnException : Exception
    {
        public PlayerEndTurnException()
        {
        }

        public PlayerEndTurnException(string? message) : base(message)
        {
        }

        public PlayerEndTurnException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected PlayerEndTurnException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}