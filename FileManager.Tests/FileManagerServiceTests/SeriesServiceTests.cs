using FileManager.Models;
using FileManager.Services;
using FileManager.Tests.Mocks;

using System;
using System.Collections.Generic;

using Xunit;

namespace FileManager.Tests.FileManagerServiceTests
{
    public class SeriesServiceTests
    {
        [Fact]
        public void GetSeries_ThenDoesNotThrow()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new List<Series>())
            };

            var seriesService = new SeriesService(new MockConfiguration(), mockHttpClientFactory);

            // Act
            var exception = Record.ExceptionAsync(() => seriesService.GetAsync());

            // Assert
            Assert.Null(exception.Result);
        }

        [Fact]
        public void GetSeriesById_GivenValidId_ThenDoesNotThrow()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new Season())
            };

            var seriesService = new SeriesService(new MockConfiguration(), mockHttpClientFactory);

            // Act
            var exception = Record.ExceptionAsync(() => seriesService.GetAsync(1));

            // Assert
            Assert.Null(exception.Result);
        }

        [Fact]
        public void GetSeriesById_GivenIdLessThanOne_ThenThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new ArgumentOutOfRangeException())
            };

            var seriesService = new SeriesService(new MockConfiguration(), mockHttpClientFactory);

            // Act
            var exception = Record.ExceptionAsync(() => seriesService.GetAsync(0));

            // Assert
            Assert.IsType<ArgumentOutOfRangeException>(exception.Result.InnerException);
        }

        [Fact]
        public void GetSeriesByName_GivenValidName_ThenDoesNotThrow()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new Season())
            };

            var seriesService = new SeriesService(new MockConfiguration(), mockHttpClientFactory);

            // Act
            var exception = Record.ExceptionAsync(() => seriesService.GetAsync("Test"));

            // Assert
            Assert.Null(exception.Result);
        }

        [Fact]
        public void GetSeriesByName_GivenInvalidName_ThenThrowsArgumentNullException()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new ArgumentNullException())
            };

            var seriesService = new SeriesService(new MockConfiguration(), mockHttpClientFactory);

            // Act
            var exception = Record.ExceptionAsync(() => seriesService.GetAsync(string.Empty));

            // Assert
            Assert.IsType<ArgumentNullException>(exception.Result.InnerException);
        }

        [Fact]
        public void SaveSeries_GivenValidSeries_ThenDoesNotThrow()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(1)
            };

            var seriesService = new SeriesService(new MockConfiguration(), mockHttpClientFactory);

            // Act
            var exception = Record.ExceptionAsync(() => seriesService.SaveAsync(new Series()));

            // Assert
            Assert.Null(exception.Result);
        }

        [Fact]
        public void SaveSeries_GivenInvalidSeries_ThenThrowsArgumentNullException()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new ArgumentNullException())
            };

            var seriesService = new SeriesService(new MockConfiguration(), mockHttpClientFactory);

            // Act
            var exception = Record.ExceptionAsync(() => seriesService.SaveAsync(null));

            // Assert
            Assert.IsType<ArgumentNullException>(exception.Result.InnerException);
        }
    }
}