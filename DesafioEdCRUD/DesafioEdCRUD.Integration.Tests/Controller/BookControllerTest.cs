using Entities.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Newtonsoft.Json;

namespace DesafioEdCRUD.Integration.Tests.Controller
{
    public class BookControllerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;


        public BookControllerTest(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllBooks_ReturnsOkResponse()
        {
            var response = await _client.GetAsync("/api/book");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetBookById_Should_ReturnsOkResponse()
        {
            var response = await _client.GetAsync("/api/book/2");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task CreateBook_Should_ReturnsCreatedResponse()
        {
            var response = await _client.PostAsync("/api/book", new StringContent(JsonConvert.SerializeObject
                            (new Book() { Title = "Test Title", Company = "Test Company", Edition = 1, PublishYear = "2000", Value = 20 }), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task UpdateBook_Should_ReturnsCreatedResponse()
        {
            var response = await _client.PostAsync("/api/book", new StringContent(JsonConvert.SerializeObject
                            (new Book() { Title = "Test Title", Company = "Test Company", Edition = 1, PublishYear = "2000", Value = 20 }), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
