using System.Runtime.Serialization;

namespace BaseMonopoly.Exceptions.StreetExceptions
{
    [Serializable]
    internal class BuildingCannotBeAddedException : Exception
    {
        public BuildingCannotBeAddedException()
        {
        }

        public BuildingCannotBeAddedException(string? message) : base(message)
        {
        }

        public BuildingCannotBeAddedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected BuildingCannotBeAddedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}