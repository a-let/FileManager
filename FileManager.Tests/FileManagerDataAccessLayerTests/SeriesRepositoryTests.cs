using FileManager.DataAccessLayer;
using FileManager.DataAccessLayer.Repositories;
using FileManager.Models;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xunit;

namespace FileManager.Tests.FileManagerDataAccessLayerTests
{
    [Collection("Database collection")]
    public class SeriesRepositoryTests
    {
        private readonly FileManagerContext _context;

        public SeriesRepositoryTests(DatabaseFixture dbFixture)
        {
            _context = dbFixture.Context;
        }

        [Fact]
        public async Task GetSeriesById_GivenValidId_ThenSeriesIsReturned()
        {
            // Arrange
            var id = 1;

            var seriesRepo = new SeriesRepository(_context);

            // Act
            var series = await seriesRepo.GetByIdAsync(id);

            // Assert
            Assert.Equal(id, series.SeriesId);
        }

        [Fact]
        public void GetSeriesByName_GivenValidName_ThenSeriesIsReturned()
        {
            // Arrange
            var name = "Test";

            var seriesRepo = new SeriesRepository(_context);

            // Act
            var series = seriesRepo.GetByName(name);

            // Assert
            Assert.IsAssignableFrom<Series>(series);
            Assert.Equal(name, series.Name);
        }

        [Fact]
        public void GetSeriess_ThenReturnsEpisodeList()
        {
            // Arrange
            var seriesRepo = new SeriesRepository(_context);

            // Act
            var seriess = seriesRepo.Get();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Series>>(seriess);
        }

        [Fact]
        public async Task SaveSeries_GivenValidNewSeries_ThenReturnsSeriesId()
        {
            // Arrange
            var series = new Series
            {
                SeriesId = 0,
                Name = "Test",
                Path = "Some Path"
            };

            var seriesRepo = new SeriesRepository(_context);

            // Act
            await seriesRepo.SaveAsync(series);

            // Assert
            Assert.True(series.SeriesId > 0);
        }

        [Fact]
        public async Task SaveSeries_GivenValidExistingSeries_ThenSeriesIdIsEqual()
        {
            // Arrange
            var series = new Series
            {
                SeriesId = 1,
                Name = "Test",
                Path = "Some Path"
            };

            var seriesRepo = new SeriesRepository(_context);

            // Act
            series.Path = "New path";

            await seriesRepo.SaveAsync(series);

            // Assert
            Assert.Equal(1, series.SeriesId);
        }

        [Fact]
        public async Task SaveSeries_GivenNonExistingSeries_ThenThrowsArgumentNullException()
        {
            // Arrange
            var series = new Series
            {
                SeriesId = 10,
                Name = "Test",
                Path = "Some Path"
            };

            var seriesRepo = new SeriesRepository(_context);

            // Act
            var exception = await Record.ExceptionAsync(async () => await seriesRepo.SaveAsync(series));

            // Assert
            Assert.IsType<ArgumentNullException>(exception);
        }
    }
}