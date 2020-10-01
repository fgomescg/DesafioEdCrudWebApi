using Entities.Models;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IBookRepository : IRepositoryBase<Book>
    {
        ValueTask<PagedList<Book>> GetAllBooks(BookParameters bookParameters);
        ValueTask<Book> GetBookById(int bookId);              
        void CreateBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(Book book);
        ValueTask<BookAuthorReport[]> GetBookAuthorReports();

    }
}
