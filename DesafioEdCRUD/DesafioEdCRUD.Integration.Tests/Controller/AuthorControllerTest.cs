using Entities.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DesafioEdCRUD.Integration.Tests.Controller
{
   
    public class AuthorControllerTest : IntegrationTest
    {       
        private Author authorTest;
        private string urlPath = "/api/author";

        public AuthorControllerTest()
        {
            authorTest = new Author() { Name = "Author Test"};
        }        

        [Fact]
        public async Task GetAllAuthors_ReturnsOkResponse()
        {
            var response = await TestClient.GetAsync(urlPath);
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetAuthorById_Should_ReturnsOkResponse()
        {
            var responsePost = await TestClient.PostAsync(urlPath, new StringContent(JsonConvert.SerializeObject
                             (authorTest), Encoding.UTF8, "application/json"));

            var getPah = responsePost.Headers.Location.AbsolutePath;

            var response = await TestClient.GetAsync(getPah);
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task CreateAuthor_Should_ReturnsCreatedResponse()
        {
            var response = await TestClient.PostAsync(urlPath, new StringContent(JsonConvert.SerializeObject
                            (authorTest), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task UpdateAuthor_Should_ReturnsNoContentResponse()
        {
            var responsePost = await TestClient.PostAsync(urlPath, new StringContent(JsonConvert.SerializeObject
                             (authorTest), Encoding.UTF8, "application/json"));

            var getPah = responsePost.Headers.Location.AbsolutePath;

            authorTest.Name = "Test Author updated";

            var response = await TestClient.PutAsync(getPah, new StringContent(JsonConvert.SerializeObject
                             (authorTest), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DeleteAuthor_Should_ReturnsNoContentResponse()
        {
            var responsePost = await TestClient.PostAsync(urlPath, new StringContent(JsonConvert.SerializeObject
                             (authorTest), Encoding.UTF8, "application/json"));

            var getPah = responsePost.Headers.Location.AbsolutePath;

            var response = await TestClient.DeleteAsync(getPah);
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
