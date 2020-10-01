using System;

namespace Entities.Models.Books.Exceptions
{
    public class NotFoundBookException : Exception
    {
        public NotFoundBookException(int bookId)
            : base($"Could not find book with ID: {bookId}") { }
    }
}
