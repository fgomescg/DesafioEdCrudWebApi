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
        ValueTask<PagedList<Book>> GetBooksAsync(BookParameters bookParameters);
        ValueTask<BookDto> GetBookByIdAsync(int bookId);
        Task<BookDto> CreateBookAsync(Book book);
        Task<bool> UpdateBookAsync(int Id, BookForUpdateDto book);
        Task<bool> DeleteBookAsync(int Id);
        ValueTask<BookAuthorReport[]> GetBookAuthorReports();

    }
}
