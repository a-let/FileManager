using FileManager.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FileManager.IntegrationTests
{
    [Collection("Integration Test Collection")]
    public class SeasonTests : TestBase
    {
        public SeasonTests() : base(new CustomWebApplicationFactory<Web.Startup>())
        { }

        [Fact]
        public async Task Get()
        {
            // Arrange

            // Act
            var responseMessage = await _client.GetAsync("api/Season");
            var strContent = await responseMessage.Content.ReadAsStringAsync();
            var seasons = DeserializeObject<IEnumerable<Season>>(strContent);

            // Assert
            Assert.NotEmpty(seasons);
        }

        [Fact]
        public async Task GetById()
        {
            // Arrange
            var seasonId = 1;

            // Act
            var responseMessage = await _client.GetAsync($"api/Season/id/{seasonId}");
            var strContent = await responseMessage.Content.ReadAsStringAsync();
            var season = DeserializeObject<Season>(strContent);

            // Assert
            Assert.NotNull(season);
            Assert.Equal(seasonId, season.SeasonId);
        }

        [Fact]
        public async Task GetByShowId()
        {
            // Arrange
            var showId = 1;

            // Act
            var responseMessage = await _client.GetAsync($"api/Season/showId/{showId}");
            var strContent = await responseMessage.Content.ReadAsStringAsync();
            var seasons = DeserializeObject<IEnumerable<Season>>(strContent);

            // Assert
            Assert.NotEmpty(seasons);
            Assert.Equal(showId, seasons.First().ShowId);
        }

        [Fact]
        public async Task Post()
        {
            // Arrange
            var season = new Season
            {
                SeasonId = 0,
                ShowId = 1,
                SeasonNumber = 2,
                Path = @"C:/Temp",
                EpisodeList = null
            };

            // Act
            var responseMessage = await _client.PostAsync("api/Season", CreateStringContent(season));
            var strContent = await responseMessage.Content.ReadAsStringAsync();
            var success = DeserializeObject<bool>(strContent);

            // Assert
            Assert.True(success);
            Assert.Equal(2, GetSeasons().Result.Count());
        }

        private async Task<IEnumerable<Season>> GetSeasons()
        {
            var responseMessage = await _client.GetAsync("api/Season");
            var strContent = await responseMessage.Content.ReadAsStringAsync();
            return DeserializeObject<IEnumerable<Season>>(strContent);
        }
    }
}