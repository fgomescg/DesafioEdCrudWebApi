using Entities.Models;
using Entities.Models.Books;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioEdCRUD.Services
{
    public interface IBookService
    {        
        ValueTask<Book> GetBookByIdAsync(int id);

        ValueTask<PagedList<Book>> GetBooks(BookParameters bookParameters);

        ValueTask<Book> CreateBookAsync(Book book);

        ValueTask<Book> UpdateBookAsync(Book book);

        ValueTask<Book> DeleteBookAsync(int id);

        

    }
}
