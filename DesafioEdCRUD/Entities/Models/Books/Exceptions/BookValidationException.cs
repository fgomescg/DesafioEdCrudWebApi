using System;

namespace Entities.Models.Books.Exceptions
{
    public class BookValidationException : Exception
    {
        public BookValidationException(Exception innerException)
            : base("Book validation error occurred, correct your request and try again", innerException)
        { }

    }
}
