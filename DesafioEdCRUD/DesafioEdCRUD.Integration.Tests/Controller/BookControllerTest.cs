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
using Xunit.Priority;
using Microsoft.AspNetCore.Mvc.Testing;

namespace DesafioEdCRUD.Integration.Tests.Controller
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class BookControllerTest
    {
        private readonly HttpClient _client;

        public BookControllerTest()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            _client = appFactory.CreateClient();
        }

        [Fact, Priority(0)]
        public async Task CreateBook_Should_ReturnsCreatedResponse()
        {
            var response = await _client.PostAsync("/api/book", new StringContent(JsonConvert.SerializeObject
                            (new Book() { Title = "Test Title", Company = "Test Company", Edition = 1, PublishYear = "2000", Value = 20 }), Encoding.UTF8, "application/json"));
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
            var response = await _client.GetAsync("/api/book/2");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }        

        [Fact, Priority(3)]
        public async Task UpdateBook_Should_ReturnsNoContentResponse()
        {
            var response = await _client.PutAsync("/api/book/2", new StringContent(JsonConvert.SerializeObject
                            (new Book() { Title = "Test Title 2", Company = "Test Company 2", Edition = 1, PublishYear = "2020", Value = 30 }), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact, Priority(4)]
        public async Task DeleteBook_Should_ReturnsNoContentResponse()
        {
            var response = await _client.DeleteAsync("/api/book/2");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
