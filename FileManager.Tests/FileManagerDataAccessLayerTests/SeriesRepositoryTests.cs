﻿using FileManager.DataAccessLayer.Repositories;
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
        private readonly SeriesRepository _seriesRepository;

        public SeriesRepositoryTests(DatabaseFixture dbFixture)
        {
            _seriesRepository = dbFixture.SeriesRepository;
        }

        [Fact]
        public async Task GetSeriesById_GivenValidId_ThenSeriesIsReturned()
        {
            //Arrange
            var id = 1;

            //Act
            var series = await _seriesRepository.GetSeriesByIdAsync(id);

            //Assert
            Assert.Equal(id, series.SeriesId);
        }

        [Fact]
        public void GetSeriesByName_GivenValidName_ThenSeriesIsReturned()
        {
            //Arrange
            var name = "Test";

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
        public async Task SaveSeries_GivenValidNewSeries_ThenReturnsSeriesId()
        {
            //Arrange
            var series = new Series
            {
                SeriesId = 0,
                Name = "Test",
                Path = "Some Path"
            };

            //Act
            var seriesId = await _seriesRepository.SaveSeriesAsync(series);

            //Assert
            Assert.True(seriesId > 0);
        }

        [Fact(Skip = "Test is just saving a new record. Fix after DAL refactor.")]
        public async Task SaveSeries_GivenValidExistingSeries_ThenSeriesIdIsEqual()
        {
            //Arrange
            var series = new Series
            {
                SeriesId = 1,
                Name = "Test",
                Path = "Some Path"
            };

            //Act
            series.Path = "New path";

            var seriesId = await _seriesRepository.SaveSeriesAsync(series);

            //Assert
            Assert.Equal(series.SeriesId, seriesId);
        }

        [Fact]
        public async Task SaveSeries_GivenNonExistingSeries_ThenThrowsArgumentNullException()
        {
            //Arrange
            var series = new Series
            {
                SeriesId = 10,
                Name = "Test",
                Path = "Some Path"
            };

            //Act
            var exception = await Record.ExceptionAsync(async () => await _seriesRepository.SaveSeriesAsync(series));

            //Assert
            Assert.IsType<ArgumentNullException>(exception);
        }
    }
}