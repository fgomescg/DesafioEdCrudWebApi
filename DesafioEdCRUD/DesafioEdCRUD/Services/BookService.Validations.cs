using Entities.Models.Books;
using Entities.Models.Books.Exceptions;
using FluentValidation;
using System.Linq;

namespace DesafioEdCRUD.Services
{
    public partial class BookService
    {
        public void ValidateBook(Book book)
        {
            ValidateBookIsNotNull(book);
            this.bookValidator.ValidateAndThrow(book);
        }

        private void ValidateBookIsNotNull(Book book)
        {
            if (book is null)
            {
                throw new NullBookException();
            }
        }

        private void ValidateBookId(int bookId)
        {
            if (bookId == 0)
            {
                throw new InvalidBookException(
                    parameterName: nameof(Book.Id),
                    parameterValue: bookId);
            }
        }

        private void ValidateStorageBooks(IQueryable<Book> storageBooks)
        {
            if (storageBooks.Count() == 0)
            {
                this.logger.LogWarn("Book storage is empty.");
            }
        }

        public class BookValidator : AbstractValidator<Book>
        {
            public BookValidator()
            {
                RuleFor(book => book).NotNull();
                RuleFor(book => book.Id).NotEmpty();
                RuleFor(book => book.Title).NotEmpty();
            }
        }

    }
}
