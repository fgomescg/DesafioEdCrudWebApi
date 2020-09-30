using System;

namespace Entities.Models.Exceptions
{
    public class BookServiceException : Exception
    {
        public BookServiceException(Exception innerException)
           : base("System error occurred, contact support.", innerException)
        {

        }
    }
}
