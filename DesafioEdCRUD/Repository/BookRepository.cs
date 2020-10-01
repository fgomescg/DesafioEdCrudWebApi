using Entities;
using Entities.Models.Books;
using Contracts;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Repository
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        private RepositoryContext repoContext;
        public BookRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
            repoContext = repositoryContext;
        }        
        public async ValueTask<PagedList<Book>> GetBooks(BookParameters bookParameters)
        {
            var books = FindAll().Include(book => book.BookAuthors)
                            .ThenInclude(ba => ba.Author)
                            .Include(su => su.BookSubjects)
                            .ThenInclude(bs => bs.Subject)
                            .OrderBy(book => book.Title);                            

              return await Task.FromResult(PagedList<Book>.ToPagedList(books,
                   bookParameters.PageNumber,
                   bookParameters.PageSize));          
        }

        public async Task<Book> GetBookById(int Id)
        {
            return await FindByCondition(book => book.Id.Equals(Id))
                             .Include(book => book.BookAuthors)
                             .ThenInclude(ba => ba.Author)
                             .Include(su => su.BookSubjects)
                             .ThenInclude(bs => bs.Subject).FirstOrDefaultAsync();
        }       
        
        public async ValueTask<Book> CreateBook(Book book)
        {            
            EntityEntry<Book> bookEntry = await this.repoContext.AddAsync(book);
            await this.repoContext.SaveChangesAsync();
            return bookEntry.Entity;
        }
        public void UpdateBook(Book book)
        {
            Update(book);
        }
        public void DeleteBook(Book book)
        {
            Delete(book);
        }       

        public async Task<BookAuthorReport[]> GetBookAuthorReports()
        {
            return await RepositoryContext.BookAuthorReport.ToArrayAsync();
        }
    }
}
