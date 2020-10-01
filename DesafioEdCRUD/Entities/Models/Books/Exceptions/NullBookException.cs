using System;

namespace Entities.Models.Books.Exceptions
{
    public class NullBookException : Exception
    {
        public NullBookException() : base("Book is Null") { }
    }
}
