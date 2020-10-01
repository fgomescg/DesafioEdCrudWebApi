using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models.Books.Exceptions
{
    public class BookServiceException : Exception
    {
        public BookServiceException(Exception innerException)
           : base("System error occurred, contact support.", innerException)
        {

        }
    }
}
