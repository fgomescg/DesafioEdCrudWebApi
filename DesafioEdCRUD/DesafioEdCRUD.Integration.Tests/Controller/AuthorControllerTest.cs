using Entities.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Priority;

namespace DesafioEdCRUD.Integration.Tests.Controller
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class AuthorControllerTest 
    {
        private readonly HttpClient _client;

        public AuthorControllerTest()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            _client = appFactory.CreateClient();
        }        

        [Fact, Priority(0)]
        public async Task GetAllAuthors_ReturnsOkResponse()
        {
            var response = await _client.GetAsync("/api/author");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact, Priority(1)]
        public async Task GetAuthorById_Should_ReturnsOkResponse()
        {
            var response = await _client.GetAsync("/api/author/2");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact, Priority(2)]
        public async Task CreateAuthor_Should_ReturnsCreatedResponse()
        {
            var response = await _client.PostAsync("/api/author", new StringContent(JsonConvert.SerializeObject
                            (new Author() { Name = "Test Author Name" }), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact, Priority(3)]
        public async Task UpdateAuthor_Should_ReturnsNoContentResponse()
        {
            var response = await _client.PutAsync("/api/author/2", new StringContent(JsonConvert.SerializeObject
                             (new Author() { Name = "Test Author Update" }), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact, Priority(4)]
        public async Task DeleteAuthor_Should_ReturnsNoContentResponse()
        {
            var response = await _client.DeleteAsync("/api/author/2");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
