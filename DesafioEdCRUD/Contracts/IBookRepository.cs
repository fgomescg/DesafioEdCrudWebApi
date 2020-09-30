using Entities.Models;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IBookRepository : IRepositoryBase<Book>
    {
        PagedList<Book> GetAllBooks(BookParameters bookParameters);
        Task<Book> GetBookById(int bookId);              
        void CreateBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(Book book);
        Task<BookAuthorReport[]> GetBookAuthorReports();

    }
}
