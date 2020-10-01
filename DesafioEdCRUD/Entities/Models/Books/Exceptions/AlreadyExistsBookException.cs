using System;

namespace Entities.Models.Books.Exceptions
{
    public class AlreadyExistsBookException : Exception
    {
        public AlreadyExistsBookException(Exception innerException)
            : base("This book already exists", innerException)
        { }
    }
}
