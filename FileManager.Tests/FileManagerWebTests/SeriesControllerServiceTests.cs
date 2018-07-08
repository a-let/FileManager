﻿using System;
using System.Collections.Generic;

using Xunit;

using FileManager.Models;
using FileManager.Tests.Mocks;
using FileManager.Web.Services;

namespace FileManager.Tests.FileManagerWebTests
{
    public class SeriesControllerServiceTests
    {
        private readonly SeriesControllerService _seriesControllerService = new SeriesControllerService(new MockSeriesAdapter());

        [Fact]
        public void GetSeriesById_GivenInvalidId_ThenThrowsArgumentException()
        {
            //Arrange
            var id = 0;

            //Act
            var exception = Record.Exception(() => _seriesControllerService.GetSeriesById(id));

            //Assert
            Assert.IsType<ArgumentException>(exception);
        }

        [Fact]
        public void GetSeriesById_GivenValidId_ThenSeriesIsReturned()
        {
            //Arrange
            var id = 1;

            //Act
            var series = _seriesControllerService.GetSeriesById(id);

            //Assert
            Assert.IsAssignableFrom<Series>(series);
        }

        [Fact]
        public void GetSeriess_ThenReturnsSeriesList()
        {
            //Arrange, Act
            var seriess = _seriesControllerService.GetSeries();

            //Assert
            Assert.IsAssignableFrom<IEnumerable<Series>>(seriess);
        }

        [Fact]
        public void GetSeriesByName_GivenInvalidName_ThenThrowsArgumentNullException()
        {
            //Arrange
            var name = string.Empty;

            //Act
            var exception = Record.Exception(() => _seriesControllerService.GetSeriesByName(name));

            //Assert
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void GetSeriesByName_GivenValidName_ThenSeriesIsReturned()
        {
            //Arrange
            var name = "Test";

            //Act
            var series = _seriesControllerService.GetSeriesByName(name);

            //Assert
            Assert.IsAssignableFrom<Series>(series);
        }

        [Fact]
        public void SaveSeries_GivenNullSeries_ThenThrowsArgumentNullReferenceException()
        {
            //Arrange
            Series series = null;

            //Act
            var exception = Record.Exception(() => _seriesControllerService.SaveSeries(series));

            //Assert
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void SaveSeries_GivenSeries_ThenReturnsTrue()
        {
            //Arrange
            var series = new Series();

            //Act
            var success = _seriesControllerService.SaveSeries(series);

            //Assert
            Assert.True(success);
        }
    }
}