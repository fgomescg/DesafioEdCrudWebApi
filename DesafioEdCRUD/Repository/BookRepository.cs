using Entities;
using Entities.Models;
using Contracts;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Repository
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }        
        public async ValueTask<PagedList<Book>> GetAllBooks(BookParameters bookParameters)
        {
            var books = FindAll();

            SearchByTitle(ref books, bookParameters.Title);

            await books.Include(book => book.BookAuthors)
                            .ThenInclude(ba => ba.Author)
                            .Include(su => su.BookSubjects)
                            .ThenInclude(bs => bs.Subject)
                            .OrderBy(book => book.Title).ToListAsync();                            

              return PagedList<Book>.ToPagedList(books.AsQueryable(),
                   bookParameters.PageNumber,
                   bookParameters.PageSize);          
        }

        private void SearchByTitle(ref IQueryable<Book> books, string bookTitle)
        {
            if (!books.Any() || string.IsNullOrWhiteSpace(bookTitle))
                return;
            books = books.Where(o => o.Title.ToLower().Contains(bookTitle.Trim().ToLower()));
        }

        public async ValueTask<Book> GetBookById(int Id)
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
