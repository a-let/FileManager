using FileManager.Models;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;

namespace FileManager.IntegrationTests
{
    [Collection("Integration Test Collection")]
    public class EpisodeTests : TestBase
    {
        public EpisodeTests() : base(new CustomWebApplicationFactory<Web.Startup>())
        { }

        [Fact]
        public async Task Get()
        {
            // Arrange

            // Act
            var responseMessage = await _client.GetAsync("api/Episode");
            var strContent = await responseMessage.Content.ReadAsStringAsync();
            var episodes = DeserializeObject<IEnumerable<Episode>>(strContent);

            // Assert
            Assert.NotEmpty(episodes);
        }

        [Fact]
        public async Task GetById()
        {
            // Arrange
            var episodeId = 1;

            // Act
            var responseMessage = await _client.GetAsync($"api/Episode/id/{episodeId}");
            var strContent = await responseMessage.Content.ReadAsStringAsync();
            var episode = DeserializeObject<Episode>(strContent);

            // Assert
            Assert.NotNull(episode);
            Assert.Equal(episodeId, episode.EpisodeId);
        }

        [Fact]
        public async Task GetByName()
        {
            // Arrange
            var episodeName = "Test Episode";

            // Act
            var responseMessage = await _client.GetAsync($"api/Episode/name/{episodeName}");
            var strContent = await responseMessage.Content.ReadAsStringAsync();
            var episode = DeserializeObject<Episode>(strContent);

            // Assert
            Assert.NotNull(episode);
            Assert.Equal(episodeName, episode.Name);
        }

        [Fact]
        public async Task GetBySeasonId()
        {
            // Arrange
            var seasonId = 1;

            // Act
            var responseMessage = await _client.GetAsync($"api/Episode/seasonId/{seasonId}");
            var strContent = await responseMessage.Content.ReadAsStringAsync();
            var episodes = DeserializeObject<IEnumerable<Episode>>(strContent);

            // Assert
            Assert.NotEmpty(episodes);
            Assert.Equal(seasonId, episodes.First().SeasonId);
        }

        [Fact]
        public async Task Post()
        {
            // Arrange
            var episode = new Episode
            {
                EpisodeId = 0,
                SeasonId = 1,
                EpisodeNumber = 2,
                Name = "Test Episode Two",
                Format = Models.Constants.FileFormatTypes.MKV,
                Path = @"C:/Temp"
            };

            // Act
            var responseMessage = await _client.PostAsync("api/Episode", CreateStringContent(episode));
            var strContent = await responseMessage.Content.ReadAsStringAsync();
            episode = DeserializeObject<Episode>(strContent);

            // Assert
            Assert.True(episode.EpisodeId > 0);
        }
    }
}