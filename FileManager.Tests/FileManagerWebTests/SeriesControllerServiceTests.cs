using FileManager.Models;
using FileManager.Tests.Mocks;
using FileManager.Web.Services;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xunit;

namespace FileManager.Tests.FileManagerWebTests
{
    public class SeriesControllerServiceTests
    {
        private readonly SeriesControllerService _seriesControllerService = new SeriesControllerService(new MockSeriesRepository());

        [Fact]
        public async Task GetSeriesById_GivenInvalidId_ThenThrowsArgumentException()
        {
            // Arrange
            var id = 0;

            // Act
            var exception = await Record.ExceptionAsync(async () => await _seriesControllerService.GetAsync(id));

            // Assert
            Assert.IsType<ArgumentException>(exception);
        }

        [Fact]
        public async Task GetSeriesById_GivenValidId_ThenSeriesIsReturned()
        {
            // Arrange
            var id = 1;

            // Act
            var series = await _seriesControllerService.GetAsync(id);

            // Assert
            Assert.IsAssignableFrom<Series>(series);
        }

        [Fact]
        public async Task GetSeriess_ThenReturnsSeriesList()
        {
            // Arrange, Act
            var seriess = await _seriesControllerService.GetAsync();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Series>>(seriess);
        }

        [Fact]
        public async Task GetSeriesByName_GivenInvalidName_ThenThrowsArgumentNullException()
        {
            // Arrange
            var name = string.Empty;

            // Act
            var exception = await Record.ExceptionAsync(async () => await _seriesControllerService.GetAsync(name));

            // Assert
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task GetSeriesByName_GivenValidName_ThenSeriesIsReturned()
        {
            // Arrange
            var name = "Test";

            // Act
            var series = await _seriesControllerService.GetAsync(name);

            // Assert
            Assert.IsAssignableFrom<Series>(series);
        }

        [Fact]
        public async Task SaveSeries_GivenNullSeries_ThenThrowsArgumentNullReferenceException()
        {
            // Arrange
            Series series = null;

            // Act
            var exception = await Record.ExceptionAsync(async () => await _seriesControllerService.SaveAsync(series));

            // Assert
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task SaveSeries_GivenSeries_ThenReturnsSeriesId()
        {
            // Arrange
            var series = new Series();

            // Act
            var seriesId = await _seriesControllerService.SaveAsync(series);

            // Assert
            Assert.True(seriesId > 0);
        }
    }
}