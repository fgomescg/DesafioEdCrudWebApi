using System;

namespace Entities.Models.Books.Exceptions
{
    public class InvalidBookException : Exception
    {
        public InvalidBookException(string parameterName, object parameterValue)
            : base($"Invalid book error occurred. parameter name: {parameterName} " +
                 $"parameter value: {parameterValue}")
        { }
    }
}
