using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.Models.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioEdCRUD.Services
{
    public partial class BookService : IBookService
    {
        private ILoggerManager logger;
        private IBookRepository repository;
        private IMapper mapper;
        private readonly BookValidator bookValidator;

        public BookService(ILoggerManager logger, IBookRepository repository, IMapper mapper)
        {
            this.logger = logger;
            this.repository = repository;
            this.mapper = mapper;
            this.bookValidator = new BookValidator();
        }

        public ValueTask<Book> CreateBookAsync(Book book) =>
        TryCatch(async () =>
        {
            ValidateBook(book);

            return await repository.CreateBook(book);
        });
        

        public ValueTask<Book> DeleteBookAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async ValueTask<PagedList<Book>> GetBooks(BookParameters bookParameters)
        {
            return await repository.GetBooks(bookParameters);
        }

        public ValueTask<Book> GetBookByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public ValueTask<Book> UpdateBookAsync(Book book)
        {
            throw new NotImplementedException();
        }
    }
}
