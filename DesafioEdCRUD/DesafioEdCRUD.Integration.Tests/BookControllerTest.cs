using Entities.Models;
using FluentAssertions;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace DesafioEdCRUD.Integration.Tests
{
    public class BookControllerTest : IntegrationTest
    {
        private readonly string baseApiUrl = "/api/book";

        [Fact]
        public async Task GetBooks_WithoutAnyPosts_ReturnsEmptyResponse()
        {
            // Arrange
            //await AuthenticateAsync();

            // Act
            var response = await TestClient.GetAsync(baseApiUrl);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<PagedList<Book>>()).Should().BeEmpty();
        }
    }
}
