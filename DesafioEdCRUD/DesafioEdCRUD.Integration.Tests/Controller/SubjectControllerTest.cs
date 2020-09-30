using Entities.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Priority;

namespace DesafioEdCRUD.Integration.Tests.Controller
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class SubjectControllerTest 
    {
        private readonly HttpClient _client;

        public SubjectControllerTest()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            _client = appFactory.CreateClient();
        }

        [Fact, Priority(0)]
        public async Task GetAllSubjects_ReturnsOkResponse()
        {
            var response = await _client.GetAsync("/api/subject");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact, Priority(1)]
        public async Task GetSubjectById_Should_ReturnsOkResponse()
        {
            var response = await _client.GetAsync("/api/subject/2");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact, Priority(2)]
        public async Task CreateSubject_Should_ReturnsCreatedResponse()
        {
            var response = await _client.PostAsync("/api/subject", new StringContent(JsonConvert.SerializeObject
                            (new Subject() { Description = "Test Description Create" }), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact, Priority(3)]
        public async Task UpdateSubject_Should_ReturnsNoContentResponse()
        {
            var response = await _client.PutAsync("/api/subject/2", new StringContent(JsonConvert.SerializeObject
                             (new Subject() { Description = "Test Description Update" }), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact, Priority(4)]
        public async Task DeleteSubject_Should_ReturnsNoContentResponse()
        {
            var response = await _client.DeleteAsync("/api/subject/2");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
