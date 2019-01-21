using System.Collections.Generic;
using Xunit;
using FileManager.Models;
using FileManager.Tests.Mocks;
using FileManager.Web.Controllers;

namespace FileManager.Tests.FileManagerWebTests
{
    public class SeriesControllerTests
    {
        private readonly SeriesController _seriesController = new SeriesController(new MockSeriesControllerService(), new MockLoggerService());

        [Fact]
        public void Get_GivenNoParameter_ThenReturnsListOfSeriess()
        {
            //Arrange

            //Act
            var seriess = _seriesController.Get();

            //Assert
            Assert.IsAssignableFrom<IEnumerable<Series>>(seriess);
        }

        [Fact]
        public void Get_GivenId_ThenSeriesIsReturned()
        {
            //Arrange
            var id = 1;

            //Act
            var series = _seriesController.GetById(id);

            //Assert
            Assert.Equal(id, series.SeriesId);
        }

        [Fact]
        public void Get_GivenName_ThenSeriesIsReturned()
        {
            //Arrange
            var name = "Test Movie";

            //Act
            var series = _seriesController.GetByName(name);

            //Assert
            Assert.Equal(name, series.Name);
        }

        [Fact]
        public void Save_GivenValidSeries_ThenReturnsSeriesId()
        {
            //Arrange
            var series = new Series
            {
                SeriesId = 1,
                Name = "Test",
                Path = "Test"
            };

            //Act
            var seriesId = _seriesController.Post(series);

            //Assert
            Assert.True(seriesId > 0);
        }
    }
}