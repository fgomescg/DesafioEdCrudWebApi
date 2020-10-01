using System;

namespace Entities.Models.Books.Exceptions
{
    public class BookDependencyException : Exception
    {
        public BookDependencyException(Exception innerException)
            : base("Service dependency error occurred, contact support", innerException)
        {

        }
    }
}
