using Entities.Models;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IBookRepository : IRepositoryBase<Book>
    {
        ValueTask<PagedList<Book>> GetAllBooks(BookParameters bookParameters);
        ValueTask<Book> GetBookById(int bookId);              
        Task CreateBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(Book book);
        ValueTask<BookAuthorReport[]> GetBookAuthorReports();

    }
}
