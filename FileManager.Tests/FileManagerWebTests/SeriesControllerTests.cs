using FileManager.Models;
using FileManager.Tests.Mocks;
using FileManager.Web.Controllers;

using System.Collections.Generic;
using System.Threading.Tasks;

using Xunit;

namespace FileManager.Tests.FileManagerWebTests
{
    public class SeriesControllerTests
    {
        private readonly SeriesController _seriesController = new SeriesController(new MockSeriesControllerService());

        [Fact]
        public async Task Get_GivenNoParameter_ThenReturnsListOfSeriess()
        {
            // Arrange

            // Act
            var seriess = (await _seriesController.Get()).GetValue();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Series>>(seriess);
        }

        [Fact]
        public async Task Get_GivenId_ThenSeriesIsReturned()
        {
            // Arrange
            var id = 1;

            // Act
            var series = (await _seriesController.GetById(id)).GetValue();

            // Assert
            Assert.Equal(id, series.SeriesId);
        }

        [Fact]
        public async Task Get_GivenName_ThenSeriesIsReturned()
        {
            // Arrange
            var name = "Test Movie";

            // Act
            var series = (await _seriesController.GetByName(name)).GetValue();

            // Assert
            Assert.Equal(name, series.Name);
        }

        [Fact]
        public async Task Save_GivenValidSeries_ThenReturnsSeriesId()
        {
            // Arrange
            var series = new Series
            {
                SeriesId = 1,
                Name = "Test",
                Path = "Test"
            };

            // Act
            var seriesId = (await _seriesController.Post(series)).GetValue();

            // Assert
            Assert.True(seriesId > 0);
        }
    }
}