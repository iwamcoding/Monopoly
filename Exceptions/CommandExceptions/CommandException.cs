﻿using System.Runtime.Serialization;

namespace BaseMonopoly.Exceptions.CommandExceptions
{
    [Serializable]
    internal class CommandException : Exception
    {
        public CommandException()
        {
        }

        public CommandException(string? message) : base(message)
        {
        }

        public CommandException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected CommandException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}