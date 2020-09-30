using Entities.Models;
using FluentAssertions;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Newtonsoft.Json;
using Xunit.Priority;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace DesafioEdCRUD.Integration.Tests.Controller
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class BookControllerTest
    {
        private readonly HttpClient _client;
        private readonly TestServer _server;
        private Book bookTest;
        private string baseApiUrl = "/api/book";


        public BookControllerTest()
        {            
            var configurationBuilder = new ConfigurationBuilder()                
                .AddJsonFile("appsettings.test.json");
            
            _server = new TestServer(new WebHostBuilder()
                .UseConfiguration(configurationBuilder.Build())
               .UseStartup<Startup>());
            _client = _server.CreateClient();
            bookTest = new Book() { Title = "Test Title", Company = "Test Company", Edition = 1, PublishYear = "2000", Value = 20 };
        }

         [Fact, Priority(0)]
        public async Task CreateBook_Should_ReturnsCreatedResponse()
        {   
            var response = await _client.PostAsync(baseApiUrl, new StringContent(JsonConvert.SerializeObject
                            (bookTest), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact, Priority(1)]
        public async Task GetAllBooks_ReturnsOkResponse()
        {         
            var response = await _client.GetAsync("/api/book");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact, Priority(2)]
        public async Task GetBookById_Should_ReturnsOkResponse()
        {    
            var responsePost = await _client.PostAsync(baseApiUrl, new StringContent(JsonConvert.SerializeObject
                            (bookTest), Encoding.UTF8, "application/json"));

            var getPah = responsePost.Headers.Location.AbsolutePath;

            var response = await _client.GetAsync(getPah);
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }        

        [Fact, Priority(3)]
        public async Task UpdateBook_Should_ReturnsNoContentResponse()
        {
            var responsePost = await _client.PostAsync(baseApiUrl, new StringContent(JsonConvert.SerializeObject
                            (bookTest), Encoding.UTF8, "application/json"));

            var bookCreatedPath = responsePost.Headers.Location.AbsolutePath;

            bookTest.Title = "Updated Title Test";

            var response = await _client.PutAsync(bookCreatedPath, new StringContent(JsonConvert.SerializeObject
                            (bookTest), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact, Priority(4)]
        public async Task DeleteBook_Should_ReturnsNoContentResponse()
        {
            var responsePost = await _client.PostAsync(baseApiUrl, new StringContent(JsonConvert.SerializeObject
                            (bookTest), Encoding.UTF8, "application/json"));

            var bookCreatedPath = responsePost.Headers.Location.AbsolutePath;

            var response = await _client.DeleteAsync(bookCreatedPath);
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
