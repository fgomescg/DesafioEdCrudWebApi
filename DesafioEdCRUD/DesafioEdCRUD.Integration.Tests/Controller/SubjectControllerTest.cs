using DesafioEdCRUD.Controllers;
using Entities.Models;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;


namespace DesafioEdCRUD.Integration.Tests.Controller
{
    public class SubjectControllerTest : IntegrationTest
    {
      
        private Subject subjectTest;       

        public SubjectControllerTest()
        {           
            subjectTest = new Subject() { Description = "Subject Test" };
        }

        [Fact]
        public async Task GetAllSubjects_ReturnsOkResponse()
        {
            await AuthenticateAsync();

            var response = await TestClient.GetAsync(ApiRoutes.Subjects.GetAll);
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetSubjectById_Should_ReturnsOkResponse()
        {
            await AuthenticateAsync();

            var responsePost = await TestClient.PostAsJsonAsync(ApiRoutes.Subjects.Create, subjectTest);

            var getPah = responsePost.Headers.Location.AbsolutePath;

            var response = await TestClient.GetAsync(getPah);
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task CreateSubject_Should_ReturnsCreatedResponse()
        {
            await AuthenticateAsync();

            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Subjects.Create, subjectTest);
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task UpdateSubject_Should_ReturnsNoContentResponse()
        {
            await AuthenticateAsync();

            var responsePost = await TestClient.PostAsJsonAsync(ApiRoutes.Subjects.Create, subjectTest);

            var getPah = responsePost.Headers.Location.AbsolutePath;

            subjectTest.Description = "Test Subject updated";

            var response = await TestClient.PutAsync(getPah, new StringContent(JsonConvert.SerializeObject
                             (subjectTest), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DeleteSubject_Should_ReturnsNoContentResponse()
        {
            await AuthenticateAsync();

            var responsePost = await TestClient.PostAsJsonAsync(ApiRoutes.Subjects.Create, subjectTest);

            var getPah = responsePost.Headers.Location.AbsolutePath;

            var response = await TestClient.DeleteAsync(getPah);
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
