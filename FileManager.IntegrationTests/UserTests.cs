using FileManager.Models.Dtos;

using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Xunit;

namespace FileManager.IntegrationTests
{
    [Collection("Integration Test Collection")]
    public class UserTests : TestBase
    {
        public UserTests() : base(new CustomWebApplicationFactory<Web.Startup>())
        { }

        [Fact]
        public async Task Get_ValidToken_ReturnsNotEmptyCollection()
        {
            // Arrange
            var token = await GetToken("JDoe", "Test123");

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Act
            var responseMessage = await _client.GetAsync("api/User");
            var strContent = await responseMessage.Content.ReadAsStringAsync();
            var users = DeserializeObject<IEnumerable<UserDto>>(strContent);

            // Assert
            Assert.NotEmpty(users);
        }

        [Fact]
        public async Task Get_InvalidToken_ReturnsUnauthorized()
        {
            // Arrange
            var invalidToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", invalidToken);

            // Act
            var responseMessage = await _client.GetAsync("api/User");

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.Unauthorized, responseMessage.StatusCode);
        }
    }
}