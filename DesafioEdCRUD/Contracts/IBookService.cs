using Entities.DTO;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IBookService
    {
        ValueTask<PagedList<BookDto>> GetBooksAsync(BookParameters bookParameters);
        ValueTask<BookDto> GetBookByIdAsync(int bookId);
        ValueTask<BookDto> CreateBookAsync(Book book);
        ValueTask<bool> UpdateBookAsync(int Id, BookPut bookPut);
        ValueTask<bool> DeleteBookAsync(int Id);
        ValueTask<BookAuthorReport[]> GetBookAuthorReports();

    }
}
