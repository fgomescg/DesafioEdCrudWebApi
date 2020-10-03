using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IBookRepository : IRepositoryBase<Book>
    {
        ValueTask<List<Book>> GetBooksAsync(BookParameters bookParameters);
        ValueTask<Book> GetBookByIdAsync(int bookId);              
        Task CreateBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(Book book);
        ValueTask<BookAuthorReport[]> GetBookAuthorReports();

    }
}
