using Entities;
using Entities.Models;
using Contracts;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Internal;

namespace Repository
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }        
        public async ValueTask<List<Book>> GetBooksAsync(BookParameters bookParameters)
        {
            var books = FindAll();

            SearchByTitle(ref books, bookParameters.Title);

            return await books.Include(book => book.BookAuthors)                            
                            .ThenInclude(ba => ba.Author)
                            .Include(su => su.BookSubjects)
                            .ThenInclude(bs => bs.Subject)
                            .OrderBy(book => book.Title).ToListAsync();
        }

        private void SearchByTitle(ref IQueryable<Book> books, string bookTitle)
        {
            if (!books.Any() || string.IsNullOrWhiteSpace(bookTitle))
                return;
            books = books.Where(o => o.Title.ToLower().Contains(bookTitle.Trim().ToLower()));
        }

        public async ValueTask<Book> GetBookByIdAsync(int Id)
        {
            return await FindByCondition(book => book.Id.Equals(Id))
                             .Include(book => book.BookAuthors)
                             .ThenInclude(ba => ba.Author)
                             .Include(su => su.BookSubjects)
                             .ThenInclude(bs => bs.Subject).FirstOrDefaultAsync();            
        }       
        
        public async Task CreateBookAsync(Book book)
        {
            await CreateAsync(book);            
        }
        public async Task UpdateBookAsync(Book book)
        {
            await UpdateAsync(book);           
        }
        public async Task DeleteBookAsync(Book book)
        {
            await DeleteAsync(book);            
        }
        public async ValueTask<BookAuthorReport[]> GetBookAuthorReports()
        {
            return await RepositoryContext.BookAuthorReport.ToArrayAsync();
        }
    }
}
