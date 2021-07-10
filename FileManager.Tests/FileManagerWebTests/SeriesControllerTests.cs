using FileManager.Models;
using FileManager.Web.Controllers;
using FileManager.Web.Services.Interfaces;

using NSubstitute;

using System.Collections.Generic;
using System.Threading.Tasks;

using Xunit;

namespace FileManager.Tests.FileManagerWebTests
{
    public class SeriesControllerTests
    {
        private readonly IControllerService<Series> _seriesControllerService;
        private readonly SeriesController _seriesController;

        public SeriesControllerTests()
        {
            _seriesControllerService = Substitute.For<IControllerService<Series>>();
            _seriesController = new SeriesController(_seriesControllerService);
        }

        [Fact]
        public void Get_GivenNoParameter_ThenReturnsListOfSeriess()
        {
            // Arrange
            _seriesControllerService.Get()
                .Returns(new List<Series>());

            // Act
            var seriess = _seriesController.Get().GetValue();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Series>>(seriess);
        }

        [Fact]
        public async Task Get_GivenId_ThenSeriesIsReturned()
        {
            // Arrange
            var id = 1;

            _seriesControllerService.GetByIdAsync(Arg.Any<int>())
                .Returns(new Series { SeriesId = id });

            // Act
            var series = (await _seriesController.GetById(id)).GetValue();

            // Assert
            Assert.Equal(id, series.SeriesId);
        }

        [Fact]
        public void Get_GivenName_ThenSeriesIsReturned()
        {
            // Arrange
            var name = "Test Movie";

            _seriesControllerService.GetByName(Arg.Any<string>())
                .Returns(new Series { Name = name });

            // Act
            var series = _seriesController.GetByName(name).GetValue();

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

            _seriesControllerService.SaveAsync(Arg.Any<Series>())
                .Returns(Task.CompletedTask);

            // Act
            series = (await _seriesController.Post(series)).GetValue();

            // Assert
            Assert.True(series.SeriesId > 0);
        }
    }
}