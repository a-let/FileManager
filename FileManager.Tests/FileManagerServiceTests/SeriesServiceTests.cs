﻿using FileManager.Models;
using FileManager.Services;
using FileManager.Tests.Mocks;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xunit;

namespace FileManager.Tests.FileManagerServiceTests
{
    public class SeriesServiceTests
    {
        [Fact]
        public async Task GetSeries_ThenDoesNotThrow()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new List<Series>())
            };

            var seriesService = new SeriesService(new MockConfiguration(), mockHttpClientFactory, new MockLog<SeriesService>());

            // Act
            var exception = await Record.ExceptionAsync(async () => await seriesService.GetAsync());

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public async Task GetSeriesById_GivenValidId_ThenDoesNotThrow()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new Season())
            };

            var seriesService = new SeriesService(new MockConfiguration(), mockHttpClientFactory, new MockLog<SeriesService>());

            // Act
            var exception = await Record.ExceptionAsync(async () => await seriesService.GetAsync(1));

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public async Task GetSeriesById_GivenIdLessThanOne_ThenThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new ArgumentOutOfRangeException())
            };

            var seriesService = new SeriesService(new MockConfiguration(), mockHttpClientFactory, new MockLog<SeriesService>());

            // Act
            var exception = await Record.ExceptionAsync(async () => await seriesService.GetAsync(0));

            // Assert
            Assert.IsType<ArgumentOutOfRangeException>(exception);
        }

        [Fact]
        public async Task GetSeriesByName_GivenValidName_ThenDoesNotThrow()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new Season())
            };

            var seriesService = new SeriesService(new MockConfiguration(), mockHttpClientFactory, new MockLog<SeriesService>());

            // Act
            var exception = await Record.ExceptionAsync(async () => await seriesService.GetAsync("Test"));

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public async Task GetSeriesByName_GivenInvalidName_ThenThrowsArgumentNullException()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new ArgumentNullException())
            };

            var seriesService = new SeriesService(new MockConfiguration(), mockHttpClientFactory, new MockLog<SeriesService>());

            // Act
            var exception = await Record.ExceptionAsync(async () => await seriesService.GetAsync(string.Empty));

            // Assert
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task SaveSeries_GivenValidSeries_ThenDoesNotThrow()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(1)
            };

            var seriesService = new SeriesService(new MockConfiguration(), mockHttpClientFactory, new MockLog<SeriesService>());

            // Act
            var exception = await Record.ExceptionAsync(async () => await seriesService.SaveAsync(new Series()));

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public async Task SaveSeries_GivenInvalidSeries_ThenThrowsArgumentNullException()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new ArgumentNullException())
            };

            var seriesService = new SeriesService(new MockConfiguration(), mockHttpClientFactory, new MockLog<SeriesService>());

            // Act
            var exception = await Record.ExceptionAsync(async () => await seriesService.SaveAsync(null));

            // Assert
            Assert.IsType<ArgumentNullException>(exception);
        }
    }
}