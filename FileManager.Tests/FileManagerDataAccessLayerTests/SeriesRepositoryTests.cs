using FileManager.DataAccessLayer.Repositories;
using FileManager.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FileManager.Tests.FileManagerDataAccessLayerTests
{
    public class SeriesRepositoryTests : TestBase
    {
        private readonly SeriesRepository _seriesRepository;

        public SeriesRepositoryTests() : base(nameof(SeriesRepositoryTests))
        {
            _seriesRepository = new SeriesRepository(_context);
        }

        [Fact]
        public void GetSeriesById_GivenValidId_ThenSeriesIsReturned()
        {
            //Arrange
            var id = 1;

            _context.Series.Add(new Series
            {
                SeriesId = 1,
                Name = "Test",
                Path = "Some Path"
            });

            _context.SaveChanges();

            //Act
            var series = _seriesRepository.GetSeriesById(id);

            //Assert
            Assert.Equal(id, series.SeriesId);
        }

        [Fact]
        public void GetSeriesByName_GivenValidName_ThenSeriesIsReturned()
        {
            //Arrange
            var name = "Test";

            _context.Series.Add(new Series
            {
                SeriesId = 0,
                Name = name,
                Path = "Some Path"
            });

            _context.SaveChanges();

            //Act
            var series = _seriesRepository.GetSeriesByName(name);

            //Assert
            Assert.IsAssignableFrom<Series>(series);
            Assert.Equal(name, series.Name);
        }

        [Fact]
        public void GetSeriess_ThenReturnsEpisodeList()
        {
            //Arrange, Act
            var seriess = _seriesRepository.GetSeries();

            //Assert
            Assert.IsAssignableFrom<IEnumerable<Series>>(seriess);
        }

        [Fact]
        public void SaveSeries_GivenValidNewSeries_ThenReturnsTrue()
        {
            //Arrange
            var series = new Series
            {
                SeriesId = 0,
                Name = "Test",
                Path = "Some Path"
            };

            //Act
            var success = _seriesRepository.SaveSeries(series);

            //Assert
            Assert.True(success);
        }

        [Fact]
        public void SaveSeries_GivenValidExistingSeries_ThenReturnsTrue()
        {
            //Arrange
            var series = new Series
            {
                SeriesId = 2,
                Name = "Test",
                Path = "Some Path"
            };

            _context.Series.Add(series);
            _context.SaveChanges();

            //Act

            series.SeriesId = 2;
            series.Name = "Updated Name";

            var success = _seriesRepository.SaveSeries(series);

            //Assert
            Assert.True(success);
        }

        [Fact]
        public void SaveSeries_GivenNonExistingSeries_ThenReturnsFalse()
        {
            //Arrange
            var series = new Series
            {
                SeriesId = 10,
                Name = "Test",
                Path = "Some Path"
            };

            //Act
            var success = _seriesRepository.SaveSeries(series);

            //Assert
            Assert.False(success);
        }

        protected override void Dispose(bool disposing = true)
        {
            base.Dispose(disposing);

            _seriesRepository.Dispose();
        }
    }
}