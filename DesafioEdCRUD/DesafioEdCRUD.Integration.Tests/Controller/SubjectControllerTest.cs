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


namespace DesafioEdCRUD.Integration.Tests.Controller
{
    public class SubjectControllerTest : IntegrationTest
    {
      
        private Subject subjectTest;
        private string baseApiUrl = "/api/subject";

        public SubjectControllerTest()
        {           
            subjectTest = new Subject() { Description = "Subject Test" };
        }

        [Fact]
        public async Task GetAllSubjects_ReturnsOkResponse()
        {
            var response = await TestClient.GetAsync(baseApiUrl);
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetSubjectById_Should_ReturnsOkResponse()
        {
            var responsePost = await TestClient.PostAsync(baseApiUrl, new StringContent(JsonConvert.SerializeObject
                             (subjectTest), Encoding.UTF8, "application/json"));

            var getPah = responsePost.Headers.Location.AbsolutePath;

            var response = await TestClient.GetAsync(getPah);
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task CreateSubject_Should_ReturnsCreatedResponse()
        {
            var response = await TestClient.PostAsync(baseApiUrl, new StringContent(JsonConvert.SerializeObject
                            (subjectTest), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task UpdateSubject_Should_ReturnsNoContentResponse()
        {
            var responsePost = await TestClient.PostAsync(baseApiUrl, new StringContent(JsonConvert.SerializeObject
                             (subjectTest), Encoding.UTF8, "application/json"));

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
            var responsePost = await TestClient.PostAsync(baseApiUrl, new StringContent(JsonConvert.SerializeObject
                             (subjectTest), Encoding.UTF8, "application/json"));

            var getPah = responsePost.Headers.Location.AbsolutePath;

            var response = await TestClient.DeleteAsync(getPah);
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
