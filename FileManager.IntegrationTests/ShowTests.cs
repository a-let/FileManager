using FileManager.Models;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;

namespace FileManager.IntegrationTests
{
    [Collection("Integration Test Collection")]
    public class ShowTests : TestBase
    {
        public ShowTests(CustomWebApplicationFactory<Web.Startup> factory) : base(factory)
        { }

        [Fact]
        public async Task Get()
        {
            // Arrange

            // Act
            var responseMessage = await _client.GetAsync("api/Show");
            var strContent = await responseMessage.Content.ReadAsStringAsync();
            var shows = DeserializeObject<IEnumerable<Show>>(strContent);

            // Assert
            Assert.NotEmpty(shows);
        }

        [Fact]
        public async Task GetById()
        {
            // Arrange
            var showId = 1;

            // Act
            var responseMessage = await _client.GetAsync($"api/Show/id/{showId}");
            var strContent = await responseMessage.Content.ReadAsStringAsync();
            var show = DeserializeObject<Show>(strContent);

            // Assert
            Assert.NotNull(show);
            Assert.Equal(showId, show.ShowId);
        }

        [Fact]
        public async Task GetByName()
        {
            // Arrange
            var showName = "Test Show";

            // Act
            var responseMessage = await _client.GetAsync($"api/Show/name/{showName}");
            var strContent = await responseMessage.Content.ReadAsStringAsync();
            var show = DeserializeObject<Show>(strContent);

            // Assert
            Assert.NotNull(show);
            Assert.Equal(showName, show.Name);
        }

        [Fact]
        public async Task Post()
        {
            // Arrange
            var show = new Show
            {
                ShowId = 0,
                Name = "Test Show Two",
                Category = "Testing",
                Path = @"C:/Temp",
                Seasons = null
            };

            // Act
            var responseMessage = await _client.PostAsync("api/Show", CreateStringContent(show));
            var strContent = await responseMessage.Content.ReadAsStringAsync();

            show = DeserializeObject<Show>(strContent);

            // Assert
            Assert.True(show.ShowId > 0);
        }
    }
}