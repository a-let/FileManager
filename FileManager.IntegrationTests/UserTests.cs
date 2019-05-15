using FileManager.Models.Dtos;

using System.Collections.Generic;
using System.Net;
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
            var users = DeserializeObject<IEnumerable<UserDto>>(await responseMessage.Content.ReadAsStringAsync());

            // Assert
            Assert.NotEmpty(users);
        }

        [Fact]
        public async Task GetById_GivenValidId_ReturnsUser()
        {
            // Arrange
            var userId = 1;
            var token = await GetToken("JDoe", "Test123");

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Act
            var responseMessage = await _client.GetAsync($"api/User/id/{userId}");
            var user = DeserializeObject<UserDto>(await responseMessage.Content.ReadAsStringAsync());

            // Assert
            Assert.Equal(userId, user.UserId);
        }

        [Fact]
        public async Task GetByUserName_GivenValidUserName_ReturnsUser()
        {
            // Arrange
            var userName = "JDoe";
            var token = await GetToken(userName, "Test123");

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Act
            var responseMessage = await _client.GetAsync($"api/User/userName/{userName}");
            var user = DeserializeObject<UserDto>(await responseMessage.Content.ReadAsStringAsync());

            // Assert
            Assert.Equal(userName, user.UserName);
        }

        [Fact]
        public async Task Post_GivenValidUser_ReturnsId()
        {
            // Arrange
            var user = new UserDto
            {
                UserName = "JDoe1",
                FirstName = "John",
                LastName = "Doe",
                Password = "Test123"
            };

            // Act
            var responseMessage = await _client.PostAsync("api/User", CreateStringContent(user));
            var userId = DeserializeObject<int>(await responseMessage.Content.ReadAsStringAsync());

            // Assert
            Assert.Equal(2, userId);
        }

        [Fact]
        public async Task Authenticate_GivenValidUser_ThenReturnsOkStatus()
        {
            // Arrange
            var user = new UserDto
            {
                UserName = "JDoe",
                Password = "Test123"
            };

            // Act
            var responseMessage = await _client.PostAsync("api/User/Authenticate", CreateStringContent(user));

            // Assert
            Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);
        }
    }
}