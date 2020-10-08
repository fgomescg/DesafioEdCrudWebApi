using AutoMapper;
using Contracts;
using DesafioEdCRUD.Services;
using Entities.DTO;
using Entities.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DesafioEdCRUD.Unit.Tests.Services
{    
    public class BookServiceTest
    {
        private readonly BookService bookService;
        private readonly Mock<IRepositoryWrapper> repoWrapperMock = new Mock<IRepositoryWrapper>();
        private readonly Mock<ILoggerManager> loggerMock = new Mock<ILoggerManager>();
        private readonly List<Book> _bookList;

        public BookServiceTest()
        {
            var myProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            var mapper = new Mapper(configuration);

            _bookList = new List<Book>()
            {
                new Book(){ Id = new Random().Next(1, int.MaxValue), Title = "Guinnes Book vol.2", Company="Ed. Abril", Edition = 2, PublishYear = "2001", Value=201m },
                new Book(){ Id = new Random().Next(1, int.MaxValue), Title = "O Navio", Company="Ed. Abril", Edition = 3, PublishYear = "2002", Value=202m },
                new Book(){ Id = new Random().Next(1, int.MaxValue), Title = "Guinnes Book vol.1", Company="Ed. Abril", Edition = 1, PublishYear = "2000", Value=200m },
                new Book(){ Id = new Random().Next(1, int.MaxValue), Title = "O Avião" , Company="Ed. Abril", Edition = 4, PublishYear = "2003", Value=203m },
                new Book(){ Id = new Random().Next(1, int.MaxValue), Title = "A Bíblia Sagrada", Company="Ed. Abril", Edition = 5, PublishYear = "1500", Value=204m }
            };

            bookService = new BookService(repoWrapperMock.Object, loggerMock.Object, mapper);
        }


        [Fact]
        public async Task GetBooksAsync_ShouldReturnPagedListBook()
        {
            //Arrange   
            var parameters = new BookParameters() { PageNumber = 0, PageSize = 10 };

            var pagedBooksMock = PagedList<Book>.ToPagedList(_bookList.AsQueryable(), 0, 10);           

            repoWrapperMock.Setup(p => p.Book.GetBooksAsync(parameters))
                .ReturnsAsync(pagedBooksMock);

            //Act
            var pagedBookList = await bookService.GetBooksAsync(parameters);            

            //Assert
            Assert.Equal(pagedBooksMock.TotalCount, pagedBookList.TotalCount);
            Assert.Equal(pagedBooksMock.TotalPages, pagedBookList.TotalPages);
            Assert.Equal(pagedBooksMock.CurrentPage, pagedBookList.CurrentPage);
            Assert.Equal(pagedBooksMock.PageSize, pagedBookList.PageSize);
            Assert.Equal(pagedBooksMock.Count, pagedBookList.Count);            
        }

        [Fact]
        public async Task GetBookByIdAsync_ShouldReturnBook_WhenBookExists()
        {
            //Arrange            
            var bookId = new Random().Next(1, int.MaxValue);
            var bookTitle = "Book test title";
            var book = new Book() { Id = bookId, Title = bookTitle, Company = "Company Test", Edition = 1, PublishYear = "2000", Value = 100m };

            repoWrapperMock.Setup(p => p.Book.GetBookByIdAsync(bookId))
                .ReturnsAsync(book);

            //Act
            var bookDto = await bookService.GetBookByIdAsync(bookId);

            //Assert
            Assert.Equal(bookId, bookDto.Id);
            Assert.Equal(bookTitle, bookDto.Title);
        }

        [Fact]
        public async Task GetBookByIdAsync_ShouldReturnNothing_WhenBookNotExists()
        {
            //Arrange                      
            repoWrapperMock.Setup(p => p.Book.GetBookByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var bookId = new Random().Next(1, int.MaxValue);
            var book = await bookService.GetBookByIdAsync(bookId);

            //Assert
            Assert.Null(book);
        }

        [Fact]
        public async Task CreateBookAsync_ShouldReturnBookDto_WhenBookCreated()
        {
            //Arrange                        
            var bookTitle = "Book test created";
            var book = new Book() {Title = bookTitle, Company = "Company Test", Edition = 1, PublishYear = "2000", Value = 100m };

            repoWrapperMock.Setup(p => p.Book.CreateBookAsync(book));

            //Act
            var bookDto = await bookService.CreateBookAsync(book);

            //Assert
            Assert.Equal(bookDto.Title, bookTitle);            
        }

        [Fact]
        public async Task UpdateBookAsync_ShouldReturnTrue_WhenBookUpdated()
        {
            //Arrange                        
            var bookId = new Random().Next(1, int.MaxValue);
            var bookTitle = "Book test title";
            var book = new Book() { Id = bookId, Title = bookTitle, Company = "Company Test", Edition = 1, PublishYear = "2000", Value = 100m };

            repoWrapperMock.Setup(p => p.Book.GetBookByIdAsync(bookId))
                .ReturnsAsync(book);

            var bookForUpdated = new BookPut() { Title = "Book test title updated" };
            repoWrapperMock.Setup(p => p.Book.UpdateBookAsync(book));

            //Act
            var isBookUpdated = await bookService.UpdateBookAsync(bookId, bookForUpdated);

            //Assert
            Assert.True(isBookUpdated);
        }

        [Fact]
        public async Task UpdateBookAsync_ShouldReturnFalse_WhenBookNotFound()
        {
            //Arrange  
            var bookId = new Random().Next(1, int.MaxValue);
            var bookForUpdated = new BookPut() { Title = "Book test title updated" };
            repoWrapperMock.Setup(p => p.Book.GetBookByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);            
            
            //Act
            var isBookUpdated = await bookService.UpdateBookAsync(bookId, bookForUpdated);

            //Assert
            Assert.False(isBookUpdated);
        }

        [Fact]
        public async Task DeleteBookAsync_ShouldReturnTrue_WhenBookDeleted()
        {
            //Arrange                        
            var bookId = new Random().Next(1, int.MaxValue);
            var bookTitle = "Book test title";
            var book = new Book() { Id = bookId, Title = bookTitle, Company = "Company Test", Edition = 1, PublishYear = "2000", Value = 100m };

            repoWrapperMock.Setup(p => p.Book.GetBookByIdAsync(bookId))
                .ReturnsAsync(book);
            
            repoWrapperMock.Setup(p => p.Book.DeleteBookAsync(book));

            //Act
            var isBookDeleted = await bookService.DeleteBookAsync(bookId);

            //Assert
            Assert.True(isBookDeleted);
        }

        [Fact]
        public async Task DeleteBookAsync_ShouldReturnFalse_WhenBookNotFound()
        {
            //Arrange  
            var bookId = new Random().Next(1, int.MaxValue);
            
            repoWrapperMock.Setup(p => p.Book.GetBookByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var isBookDeleted = await bookService.DeleteBookAsync(bookId);

            //Assert
            Assert.False(isBookDeleted);
        }
    }
}
