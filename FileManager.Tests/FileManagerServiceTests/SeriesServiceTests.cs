using System;

using Xunit;

using FileManager.Models;
using FileManager.Services;
using FileManager.Tests.Mocks;

namespace FileManager.Tests.FileManagerServiceTests
{
    public class SeriesServiceTests
    {
        //private readonly SeriesService _seriesService = new SeriesService(new MockConfiguration(), new MockHttpClientFactory());
        private readonly SeriesService _seriesService = new SeriesService(new MockConfiguration(), null);

        [Fact]
        public void GetSeries_ThenDoesNotThrow()
        {
            //Arrange, Act
            var exception = Record.Exception(() => _seriesService.GetSeries());

            //Assert
            Assert.Null(exception);
        }

        [Fact]
        public void GetSeriesById_GivenValidId_ThenDoesNotThrow()
        {
            //Arrange
            var id = 1;

            //Act
            var exception = Record.Exception(() => _seriesService.GetSeriesById(id));

            //Assert
            Assert.Null(exception);
        }

        [Fact]
        public void GetSeriesById_GivenIdLessThanOne_ThenThrowsArgumentOutOfRangeException()
        {
            //Arrange
            var id = 0;

            //Act
            var exception = Record.Exception(() => _seriesService.GetSeriesById(id));

            //Assert
            Assert.IsType<ArgumentOutOfRangeException>(exception.InnerException);
        }

        [Fact]
        public void GetSeriesByName_GivenValidName_ThenDoesNotThrow()
        {
            //Arrange
            var name = "Test Series Name";

            //Act
            var exception = Record.Exception(() => _seriesService.GetSeriesByName(name));

            //Assert
            Assert.Null(exception);
        }

        [Fact]
        public void GetSeriesByName_GivenInvalidName_ThenThrowsArgumentNullException()
        {
            //Arrange
            var name = string.Empty;

            //Act
            var exception = Record.Exception(() => _seriesService.GetSeriesByName(name));

            //Assert
            Assert.IsType<ArgumentNullException>(exception.InnerException);
        }

        [Fact]
        public void SaveSeries_GivenValidSeries_ThenDoesNotThrow()
        {
            //Arrange
            var series = new Series();

            //Act
            var exception = Record.Exception(() => _seriesService.SaveSeries(series));

            //Assert
            Assert.Null(exception);
        }

        [Fact]
        public void SaveSeries_GivenInvalidSeries_ThenThrowsArgumentNullException()
        {
            //Arrange, Act
            var exception = Record.Exception(() => _seriesService.SaveSeries(null));

            //Assert
            Assert.IsType<ArgumentNullException>(exception.InnerException);
        }
    }
}