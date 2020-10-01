using Entities.Models;
using Entities.Models.Books;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IBookRepository : IRepositoryBase<Book>
    {
        ValueTask<PagedList<Book>> GetBooks(BookParameters bookParameters);
        Task<Book> GetBookById(int bookId);              
        ValueTask<Book> CreateBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(Book book);
        Task<BookAuthorReport[]> GetBookAuthorReports();
    }
}
