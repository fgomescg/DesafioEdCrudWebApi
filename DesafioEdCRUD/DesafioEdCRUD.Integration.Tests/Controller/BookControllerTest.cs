using Entities.Models;
using FluentAssertions;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace DesafioEdCRUD.Integration.Tests.Controller
{
    public class BookControllerTest : IntegrationTest
    {        
        private Book bookTest;
        private string baseApiUrl = "/api/book";

        public BookControllerTest()
        {            
            var configurationBuilder = new ConfigurationBuilder()                
                .AddJsonFile("appsettings.test.json");           
          
            bookTest = new Book() { Title = "Test Title", Company = "Test Company", Edition = 1, PublishYear = "2000", Value = 20 };
        }

         [Fact]
        public async Task CreateBook_Should_ReturnsCreatedResponse()
        {   
            var response = await TestClient.PostAsync(baseApiUrl, new StringContent(JsonConvert.SerializeObject
                            (bookTest), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task GetAllBooks_ReturnsOkResponse()
        {         
            var response = await TestClient.GetAsync("/api/book");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetBookById_Should_ReturnsOkResponse()
        {    
            var responsePost = await TestClient.PostAsync(baseApiUrl, new StringContent(JsonConvert.SerializeObject
                            (bookTest), Encoding.UTF8, "application/json"));

            var getPah = responsePost.Headers.Location.AbsolutePath;

            var response = await TestClient.GetAsync(getPah);
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }        

        [Fact]
        public async Task UpdateBook_Should_ReturnsNoContentResponse()
        {
            var responsePost = await TestClient.PostAsync(baseApiUrl, new StringContent(JsonConvert.SerializeObject
                            (bookTest), Encoding.UTF8, "application/json"));

            var bookCreatedPath = responsePost.Headers.Location.AbsolutePath;

            bookTest.Title = "Updated Title Test";

            var response = await TestClient.PutAsync(bookCreatedPath, new StringContent(JsonConvert.SerializeObject
                            (bookTest), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DeleteBook_Should_ReturnsNoContentResponse()
        {
            var responsePost = await TestClient.PostAsync(baseApiUrl, new StringContent(JsonConvert.SerializeObject
                            (bookTest), Encoding.UTF8, "application/json"));

            var bookCreatedPath = responsePost.Headers.Location.AbsolutePath;

            var response = await TestClient.DeleteAsync(bookCreatedPath);
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
