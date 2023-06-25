using System.Runtime.Serialization;

namespace BaseMonopoly.Exceptions.JailExceptions
{
    [Serializable]
    internal class JailBreakOutException : Exception
    {
        public JailBreakOutException()
        {
        }

        public JailBreakOutException(string? message) : base(message)
        {
        }

        public JailBreakOutException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected JailBreakOutException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}