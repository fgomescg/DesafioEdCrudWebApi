using Contracts;
using Entities;
using Entities.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DesafioEdCRUD.Unit.Tests
{
    public class BookRepositoryFake : RepositoryBase<Book>, IBookRepository
    {
        private readonly List<Book> _bookList;


        private static readonly Random getrandom = new Random();

        public static int GetRandomNumber(int max)
        {
            lock (getrandom) // synchronize
            {
                return getrandom.Next(1, max);
            }
        }

        public BookRepositoryFake(RepositoryContext repositoryContext)
            : base(repositoryContext)          
        {
            _bookList = new List<Book>()
            {
                new Book(){ Id = GetRandomNumber(int.MaxValue), Title = "Guinnes Book vol.2", Company="Ed. Abril", Edition = 2, PublishYear = "2001", Value=201m },
                new Book(){ Id = GetRandomNumber(int.MaxValue), Title = "Guinnes Book vol.3", Company="Ed. Abril", Edition = 3, PublishYear = "2002", Value=202m },
                new Book(){ Id = GetRandomNumber(int.MaxValue), Title = "Guinnes Book vol.1", Company="Ed. Abril", Edition = 1, PublishYear = "2000", Value=200m },
                new Book(){ Id = GetRandomNumber(int.MaxValue), Title = "Guinnes Book vol.4" , Company="Ed. Abril", Edition = 4, PublishYear = "2003", Value=203m },
                new Book(){ Id = GetRandomNumber(int.MaxValue), Title = "Guinnes Book vol.5", Company="Ed. Abril", Edition = 5, PublishYear = "2004", Value=204m }
            };
        }

        public ValueTask<PagedList<Book>> GetAllBooks(BookParameters bookParameters)
        {
            throw new NotImplementedException();
        }

        public ValueTask<BookAuthorReport[]> GetBookAuthorReports()
        {
            throw new NotImplementedException();
        }

        public ValueTask<Book> GetBookById(int bookId)
        {
            return new ValueTask<Book>(_bookList.Where(a => a.Id == bookId).FirstOrDefault());             
        }
        public async Task CreateBookAsync(Book book)
        {
            book.Id = GetRandomNumber(int.MaxValue);
            _bookList.Add(book);
            await Task.FromResult(book);
        }

        public Task UpdateBookAsync(Book book)
        {
            throw new NotImplementedException();
        }
        public async Task DeleteBookAsync(Book book)
        {
            var existing = _bookList.First(a => a.Id == book.Id);
            _bookList.Remove(existing);
            await Task.FromResult(book);
        }       
    }
}
