using FileManager.DataAccessLayer.Interfaces;
using FileManager.Models;
using FileManager.Web.Services;

using NSubstitute;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xunit;

namespace FileManager.Tests.FileManagerWebTests
{
    public class SeriesControllerServiceTests
    {
        private readonly IRepository<Series> _seriesRepo;
        private readonly SeriesControllerService _seriesControllerService;

        public SeriesControllerServiceTests()
        {
            _seriesRepo = Substitute.For<IRepository<Series>>();
            _seriesControllerService = new SeriesControllerService(_seriesRepo);
        }

        [Fact]
        public async Task GetSeriesById_GivenInvalidId_ThenThrowsArgumentException()
        {
            // Arrange
            var id = 0;

            // Act
            var exception = await Record.ExceptionAsync(async () => await _seriesControllerService.GetByIdAsync(id));

            // Assert
            Assert.IsType<ArgumentException>(exception);
        }

        [Fact]
        public async Task GetSeriesById_GivenValidId_ThenSeriesIsReturned()
        {
            // Arrange
            var id = 1;

            _seriesRepo.GetByIdAsync(Arg.Any<int>())
                .Returns(new Series());

            // Act
            var series = await _seriesControllerService.GetByIdAsync(id);

            // Assert
            Assert.IsAssignableFrom<Series>(series);
        }

        [Fact]
        public void GetSeriess_ThenReturnsSeriesList()
        {
            // Arrange
            _seriesRepo.Get()
                .Returns(new List<Series>());
            
            // Act
            var seriess = _seriesControllerService.Get();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Series>>(seriess);
        }

        [Fact]
        public void GetSeriesByName_GivenInvalidName_ThenThrowsArgumentNullException()
        {
            // Arrange
            var name = string.Empty;

            // Act
            var exception = Record.Exception(() => _seriesControllerService.GetByName(name));

            // Assert
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void GetSeriesByName_GivenValidName_ThenSeriesIsReturned()
        {
            // Arrange
            var name = "Test";

            _seriesRepo.GetByName(Arg.Any<string>())
                .Returns(new Series());

            // Act
            var series = _seriesControllerService.GetByName(name);

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

            _seriesRepo.SaveAsync(Arg.Any<Series>())
                .Returns(Task.CompletedTask);

            // Act
            var exception = await Record.ExceptionAsync(async () => await _seriesControllerService.SaveAsync(series));

            // Assert
            Assert.Null(exception);
        }
    }
}