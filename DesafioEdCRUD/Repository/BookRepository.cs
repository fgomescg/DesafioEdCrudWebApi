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
        public async Task<Book[]> GetAllBooks(BookParameters bookParameters, bool withAuthors = false, bool withSubjects = false)
        {
            var query = FindAll();
           
            if(withAuthors)
            {
                query = query.Include(book => book.BookAuthors)
                             .ThenInclude(ba => ba.Author);
            }

            if(withSubjects)
            {
                query = query.Include(su => su.BookSubjects)
                             .ThenInclude(bs => bs.Subject);
            }

            return await query
                    .OrderBy(bk => bk.Title)
                    .Skip((bookParameters.PageNumber - 1) * bookParameters.PageSize)
                    .Take(bookParameters.PageSize)
                    .ToArrayAsync();
        }       

        public async Task<Book> GetBookById(int Id)
        {
            return await FindByCondition(book => book.Id.Equals(Id)).FirstOrDefaultAsync();
        }       
        
        public void CreateBook(Book book)
        {
            Create(book);
        }
        public void UpdateBook(Book book)
        {
            Update(book);
        }
        public void DeleteBook(Book book)
        {
            Delete(book);
        }
    }
}