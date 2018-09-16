using System.Collections.Generic;
using Xunit;
using FileManager.Models;
using FileManager.Tests.Mocks;
using FileManager.Web.Controllers;

namespace FileManager.Tests.FileManagerWebTests
{
    public class SeriesControllerTests
    {
        private readonly SeriesController _seriesController = new SeriesController(new MockSeriesControllerService());

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
            var series = _seriesController.Get(id);

            //Assert
            Assert.Equal(id, series.SeriesId);
        }

        [Fact]
        public void Get_GivenName_ThenSeriesIsReturned()
        {
            //Arrange
            var name = "Test Movie";

            //Act
            var series = _seriesController.Get(name);

            //Assert
            Assert.Equal(name, series.Name);
        }

        [Fact]
        public void Save_GivenValidSeries_ThenReturnsTrue()
        {
            //Arrange
            var series = new Series
            {
                SeriesId = 1,
                Name = "Test",
                Path = "Test"
            };

            //Act
            var success = _seriesController.Post(series);

            //Assert
            Assert.True(success);
        }
    }
}