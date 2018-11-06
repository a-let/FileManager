using FileManager.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FileManager.IntegrationTests
{
    public class ShowTests : TestBase
    {
        public ShowTests() : base(new CustomWebApplicationFactory<Web.Startup>())
        { }

        [Fact]
        public async void Get()
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
        public async void GetById()
        {
            // Arrange
            var shows = await GetShows();
            var showId = shows.First().ShowId;

            // Act
            var responseMessage = await _client.GetAsync($"api/Show/id/{showId}");
            var strContent = await responseMessage.Content.ReadAsStringAsync();
            var show = DeserializeObject<Show>(strContent);

            // Assert
            Assert.NotNull(show);
            Assert.Equal(showId, show.ShowId);
        }

        [Fact]
        public async void GetByName()
        {
            // Arrange
            var shows = await GetShows();
            var showName = shows.First().Name;

            // Act
            var responseMessage = await _client.GetAsync($"api/Show/name/{showName}");
            var strContent = await responseMessage.Content.ReadAsStringAsync();
            var show = DeserializeObject<Show>(strContent);

            // Assert
            Assert.NotNull(show);
            Assert.Equal(showName, show.Name);
        }

        [Fact]
        public async void Post()
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
            var success = DeserializeObject<bool>(strContent);

            // Assert
            Assert.True(success);
            Assert.NotNull(GetShows().Result.Where(s => s.Name == show.Name));
        }

        private async Task<IEnumerable<Show>> GetShows()
        {
            var responseMessage = await _client.GetAsync("api/Show");
            var strContent = await responseMessage.Content.ReadAsStringAsync();
            return DeserializeObject<IEnumerable<Show>>(strContent);
        }
    }
}