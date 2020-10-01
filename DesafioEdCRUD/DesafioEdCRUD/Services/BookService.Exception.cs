using Entities.Models.Books;
using Entities.Models.Books.Exceptions;
using MySql.Data.MySqlClient;
using System;
using System.Linq;
using System.Threading.Tasks;
using EFxceptions.Models.Exceptions;
using Microsoft.EntityFrameworkCore;
using FluentValidation;

namespace DesafioEdCRUD.Services
{
    public partial class BookService 
    {
        private delegate ValueTask<Book> ReturningBookFunction();
        private delegate IQueryable<Book> ReturningBooksFunction();


        private async ValueTask<Book> TryCatch(ReturningBookFunction returningBookFunction) 
        {
            try
            {
                return await returningBookFunction();
            }
            catch (NullBookException nullBookException)
            {
                throw CreateAndLogValidationException(nullBookException);
            }
            catch (InvalidBookException invalidBookException)
            {
                throw CreateAndLogValidationException(invalidBookException);
            }
            catch (MySqlException mySqlException)
            {
                throw CreateAndLogDataBaseException(mySqlException);
            }
            catch (NotFoundBookException BookNotFoundException)
            {
                throw CreateAndLogValidationException(BookNotFoundException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var BookAlreadyExistsException =
                    new AlreadyExistsBookException(duplicateKeyException);

                throw CreateAndLogValidationException(BookAlreadyExistsException);
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw CreateAndLogDataBaseException(dbUpdateException);
            }
            catch (ValidationException validationException)
            {
                throw CreateAndLogValidationException(validationException);
            }
            catch (Exception exception)
            {
                throw CreateAndLogServiceException(exception);
            }

        }

        private BookDependencyException CreateAndLogDataBaseException(Exception exception)
        {
            var bookDependencyException = new BookDependencyException(exception);
            this.logger.LogError(bookDependencyException);

            return bookDependencyException;
        }

        private BookValidationException CreateAndLogValidationException(Exception exception)
        {
            var bookValidationException = new BookValidationException(exception);
            this.logger.LogError(bookValidationException);

            return bookValidationException;
        }

        private BookServiceException CreateAndLogServiceException(Exception exception)
        {
            var bookServiceException = new BookServiceException(exception);

            this.logger.LogError(bookServiceException);

            return bookServiceException;
        }

    }
}
