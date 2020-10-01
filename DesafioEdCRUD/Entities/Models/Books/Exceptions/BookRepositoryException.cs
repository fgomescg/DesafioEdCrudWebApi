using System;

namespace Entities.Models.Books.Exceptions
{
    public class BookRepositoryException : Exception
    {
        public BookRepositoryException(Exception innerException)
           : base("System error occurred, contact support.", innerException)
        {

        }
    }
}
