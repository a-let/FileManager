using FileManager.Models;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;

namespace FileManager.IntegrationTests
{
    [Collection("Integration Test Collection")]
    public class SeriesTests : TestBase
    {
        public SeriesTests(CustomWebApplicationFactory<Web.Startup> factory) : base(factory)
        { }

        [Fact]
        public async Task Get()
        {
            // Arrange

            // Act
            var responseMessage = await _client.GetAsync("api/Series");
            var strContent = await responseMessage.Content.ReadAsStringAsync();
            var series = DeserializeObject<IEnumerable<Series>>(strContent);

            // Assert
            Assert.NotEmpty(series);
        }

        [Fact]
        public async Task GetById()
        {
            // Arrange
            var seriesId = 1;

            // Act
            var responseMessage = await _client.GetAsync($"api/Series/id/{seriesId}");
            var strContent = await responseMessage.Content.ReadAsStringAsync();
            var series = DeserializeObject<Series>(strContent);

            // Assert
            Assert.NotNull(series);
            Assert.Equal(seriesId, series.SeriesId);
        }

        [Fact]
        public async Task GetByName()
        {
            // Arrange
            var seriesName = "Test Series";

            // Act
            var responseMessage = await _client.GetAsync($"api/Series/name/{seriesName}");
            var strContent = await responseMessage.Content.ReadAsStringAsync();
            var series = DeserializeObject<Series>(strContent);

            // Assert
            Assert.NotNull(series);
            Assert.Equal(seriesName, series.Name);
        }

        [Fact]
        public async Task Post()
        {
            // Arrange
            var series = new Series
            {
                SeriesId = 0,
                Name = "Test Series Two",
                Path = @"C:/Temp"
            };

            // Act
            var responseMessage = await _client.PostAsync("api/Series", CreateStringContent(series));
            var strContent = await responseMessage.Content.ReadAsStringAsync();

            series = DeserializeObject<Series>(strContent);

            // Assert
            Assert.True(series.SeriesId > 0);
        }
    }
}