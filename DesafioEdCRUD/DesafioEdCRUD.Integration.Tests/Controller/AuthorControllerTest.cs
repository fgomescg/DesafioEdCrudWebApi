using Entities.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
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
        private readonly TestServer _server;
        private Author authorTest;
        private string urlPath = "/api/author";

        public AuthorControllerTest()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json");

            _server = new TestServer(new WebHostBuilder()
                .UseConfiguration(configurationBuilder.Build())
               .UseStartup<Startup>());
            _client = _server.CreateClient();
            authorTest = new Author() { Name = "Author Test"};
        }        

        [Fact, Priority(0)]
        public async Task GetAllAuthors_ReturnsOkResponse()
        {
            var response = await _client.GetAsync(urlPath);
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact, Priority(1)]
        public async Task GetAuthorById_Should_ReturnsOkResponse()
        {
            var responsePost = await _client.PostAsync(urlPath, new StringContent(JsonConvert.SerializeObject
                             (authorTest), Encoding.UTF8, "application/json"));

            var getPah = responsePost.Headers.Location.AbsolutePath;

            var response = await _client.GetAsync(getPah);
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact, Priority(2)]
        public async Task CreateAuthor_Should_ReturnsCreatedResponse()
        {
            var response = await _client.PostAsync(urlPath, new StringContent(JsonConvert.SerializeObject
                            (authorTest), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact, Priority(3)]
        public async Task UpdateAuthor_Should_ReturnsNoContentResponse()
        {
            var responsePost = await _client.PostAsync(urlPath, new StringContent(JsonConvert.SerializeObject
                             (authorTest), Encoding.UTF8, "application/json"));

            var getPah = responsePost.Headers.Location.AbsolutePath;

            authorTest.Name = "Test Author updated";

            var response = await _client.PutAsync(getPah, new StringContent(JsonConvert.SerializeObject
                             (authorTest), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact, Priority(4)]
        public async Task DeleteAuthor_Should_ReturnsNoContentResponse()
        {
            var responsePost = await _client.PostAsync(urlPath, new StringContent(JsonConvert.SerializeObject
                             (authorTest), Encoding.UTF8, "application/json"));

            var getPah = responsePost.Headers.Location.AbsolutePath;

            var response = await _client.DeleteAsync(getPah);
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
