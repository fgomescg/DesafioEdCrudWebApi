using System;

namespace Entities.Models.Exceptions
{
    public class InvalidBookException : Exception
    {
        public InvalidBookException(string parameterName, object parameterValue)
            : base($"Invalid book error occurred. parameter name: {parameterName} " +
                 $"parameter value: {parameterValue}")
        { }
    }
}
