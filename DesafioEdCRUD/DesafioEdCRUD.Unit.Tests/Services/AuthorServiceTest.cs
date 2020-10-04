using AutoMapper;
using Contracts;
using DesafioEdCRUD.Services;
using Entities.DTO;
using Entities.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DesafioEdCRUD.Unit.Tests.Services
{
    public class AuthorServiceTest
    {
        private readonly AuthorService authorService;
        private readonly Mock<IRepositoryWrapper> repoWrapperMock = new Mock<IRepositoryWrapper>();
        private readonly Mock<ILoggerManager> loggerMock = new Mock<ILoggerManager>();
        private readonly List<Author> _authorList;

        public AuthorServiceTest()
        {
            var myProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            var mapper = new Mapper(configuration);

            _authorList = new List<Author>()
            {
                new Author(){ AuthorId = new Random().Next(1, int.MaxValue), Name = "Author Test 1"},
                new Author(){ AuthorId = new Random().Next(1, int.MaxValue), Name = "Author Test 2"},
                new Author(){ AuthorId = new Random().Next(1, int.MaxValue), Name = "Author Test 3"},
                new Author(){ AuthorId = new Random().Next(1, int.MaxValue), Name = "Author Test 4"},
                new Author(){ AuthorId = new Random().Next(1, int.MaxValue), Name = "Author Test 5"}
            };

            authorService = new AuthorService(repoWrapperMock.Object, loggerMock.Object, mapper);
        }

        [Fact]
        public async Task GetAuthorsAsync_ShouldReturnPagedListAuthors()
        {
            //Arrange   
            var parameters = new AuthorParameters() { PageNumber = 1, PageSize = 10 };

            var pagedAuthorsMock = PagedList<Author>.ToPagedList(_authorList.AsQueryable(), 1, 10);

            repoWrapperMock.Setup(p => p.Author.GetAuthorsAsync(parameters))
                .ReturnsAsync(pagedAuthorsMock);

            //Act
            var pagedListAuthor = await authorService.GetAuthorsAsync(parameters);

            //Assert
            Assert.Equal(pagedAuthorsMock.TotalCount, pagedListAuthor.TotalCount);
            Assert.Equal(pagedAuthorsMock.TotalPages, pagedListAuthor.TotalPages);
            Assert.Equal(pagedAuthorsMock.CurrentPage, pagedListAuthor.CurrentPage);
            Assert.Equal(pagedAuthorsMock.PageSize, pagedListAuthor.PageSize);
            Assert.Equal(pagedAuthorsMock.Count, pagedListAuthor.Count);
        }

        [Fact]
        public async Task CreateAuthorAsync_ShouldReturnAuthorDto_WhenAuthorCreated()
        {
            //Arrange                        
            var authorName = "Author Name created";
            var author = new Author() { AuthorId = new Random().Next(1, int.MaxValue), Name = authorName };

            repoWrapperMock.Setup(p => p.Author.CreateAuthorAsync(author));

            //Act
            var authorDto = await authorService.CreateAuthorAsync(author);

            //Assert
            Assert.Equal(authorDto.Name, authorName);
        }

        [Fact]
        public async Task GetAuthorByIdAsync_ShouldReturnAuthor_WhenAuthorExists()
        {
            //Arrange            
            var authorId = new Random().Next(1, int.MaxValue);
            var authorName = "AuthorName Test";
            var author = new Author() { AuthorId = authorId, Name = authorName };

            repoWrapperMock.Setup(p => p.Author.GetAuthorByIdAsync(authorId))
                .ReturnsAsync(author);

            //Act
            var authorDto = await authorService.GetAuthorByIdAsync(authorId);

            //Assert
            Assert.Equal(authorId, authorDto.AuthorId);
            Assert.Equal(authorName, authorDto.Name);
        }

        [Fact]
        public async Task GetAuthorByIdAsync_ShouldReturnNothing_WhenAuthorNotExists()
        {
            //Arrange                      
            repoWrapperMock.Setup(p => p.Author.GetAuthorByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var authorId = new Random().Next(1, int.MaxValue);
            var author = await authorService.GetAuthorByIdAsync(authorId);

            //Assert
            Assert.Null(author);
        }

        [Fact]
        public async Task UpdateAuthorAsync_ShouldReturnTrue_WhenAuthorUpdated()
        {
            //Arrange                        
            var authorId = new Random().Next(1, int.MaxValue);
            var authorName = "Author Name created";
            var author = new Author() { AuthorId = new Random().Next(1, int.MaxValue), Name = authorName };
            

            repoWrapperMock.Setup(p => p.Author.GetAuthorByIdAsync(authorId))
                .ReturnsAsync(author);

            var authorForUpdated = new AuthorPut() { Name = "Author Name Update" };
            repoWrapperMock.Setup(p => p.Author.UpdateAuthorAsync(author));

            //Act
            var isAuthorUpdated = await authorService.UpdateAuthorAsync(authorId, authorForUpdated);

            //Assert
            Assert.True(isAuthorUpdated);
        }

        [Fact]
        public async Task UpdateAuthorAsync_ShouldReturnFalse_WhenAuthorNotFound()
        {
            //Arrange  
            var authorId = new Random().Next(1, int.MaxValue);
            var authorForUpdated = new AuthorPut() { Name = "Author Name Update" };
            repoWrapperMock.Setup(p => p.Author.GetAuthorByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var isAuthorUpdated = await authorService.UpdateAuthorAsync(authorId, authorForUpdated);

            //Assert
            Assert.False(isAuthorUpdated);
        }

        [Fact]
        public async Task DeleteAuthorAsync_ShouldReturnTrue_WhenAuthorDeleted()
        {
            //Arrange                        
            var authorId = new Random().Next(1, int.MaxValue);
            var authorName = "Author test title";
            var author = new Author() { AuthorId = authorId, Name = authorName};

            repoWrapperMock.Setup(p => p.Author.GetAuthorByIdAsync(authorId))
                .ReturnsAsync(author);

            repoWrapperMock.Setup(p => p.Author.DeleteAuthorAsync(author));

            //Act
            var isAuthorDeleted = await authorService.DeleteAuthorAsync(authorId);

            //Assert
            Assert.True(isAuthorDeleted);
        }

        [Fact]
        public async Task DeleteAuthorAsync_ShouldReturnFalse_WhenAuthorNotFound()
        {
            //Arrange  
            var authorId = new Random().Next(1, int.MaxValue);

            repoWrapperMock.Setup(p => p.Author.GetAuthorByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var isBookDeleted = await authorService.DeleteAuthorAsync(authorId);

            //Assert
            Assert.False(isBookDeleted);
        }
    }
}
