using Entities.Models;
using FluentAssertions;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Newtonsoft.Json;
using DesafioEdCRUD.Controllers;

namespace DesafioEdCRUD.Integration.Tests.Controller
{
    public class BookControllerTest : IntegrationTest
    {        
        private Book bookTest;        

        public BookControllerTest()
        {             
            bookTest = new Book() { Title = "Test Title", Company = "Test Company", Edition = 1, PublishYear = "2000", Value = 20 };
        }

         [Fact]
        public async Task CreateBook_Should_ReturnsCreatedResponse()
        {
            await AuthenticateAsync();

            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Books.Create, bookTest);
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task GetAllBooks_ReturnsOkResponse()
        {
            await AuthenticateAsync();
            var response = await TestClient.GetAsync(ApiRoutes.Books.GetAll);
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetBookById_Should_ReturnsOkResponse()
        {
            await AuthenticateAsync();
            var responsePost = await TestClient.PostAsJsonAsync(ApiRoutes.Books.Create, bookTest);
            var getPah = responsePost.Headers.Location.AbsolutePath;
            var response = await TestClient.GetAsync(getPah);
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }        

        [Fact]
        public async Task UpdateBook_Should_ReturnsNoContentResponse()
        {
            await AuthenticateAsync();
            var responsePost = await TestClient.PostAsJsonAsync(ApiRoutes.Books.Create, bookTest);
            var bookCreatedPath = responsePost.Headers.Location.AbsolutePath;

            bookTest.Title = "Updated Title Test";
            var response = await TestClient.PutAsJsonAsync(bookCreatedPath, bookTest);
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DeleteBook_Should_ReturnsNoContentResponse()
        {
            await AuthenticateAsync();
            var responsePost = await TestClient.PostAsJsonAsync(ApiRoutes.Books.Create, bookTest);

            var bookCreatedPath = responsePost.Headers.Location.AbsolutePath;

            var response = await TestClient.DeleteAsync(bookCreatedPath);
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
