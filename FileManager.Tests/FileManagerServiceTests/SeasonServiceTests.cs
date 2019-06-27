using FileManager.Models;
using FileManager.Services;
using FileManager.Tests.Mocks;

using System;
using System.Collections.Generic;

using Xunit;

namespace FileManager.Tests.FileManagerServiceTests
{
    public class SeasonServiceTests
    {
        [Fact]
        public void GetBySeasonId_GivenValidSeasonId_ThenDoesNotThrow()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new Season())
            };

            var seasonService = new SeasonService(new MockConfiguration(), mockHttpClientFactory);

            // Act
            var exception = Record.ExceptionAsync(() => seasonService.GetSeasonById(1));

            // Assert
            Assert.Null(exception.Result);
        }

        [Fact]
        public void GetBySeasonId_GivenIdLessThanOne_ThenThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new ArgumentOutOfRangeException())
            };

            var seasonService = new SeasonService(new MockConfiguration(), mockHttpClientFactory);

            // Act
            var exception = Record.ExceptionAsync(() => seasonService.GetSeasonById(0));

            // Assert
            Assert.IsType<ArgumentOutOfRangeException>(exception.Result.InnerException);
        }

        [Fact]
        public void GetSeasons_ThenDoesNotThrow()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new List<Season>())
            };

            var seasonService = new SeasonService(new MockConfiguration(), mockHttpClientFactory);

            // Act
            var exception = Record.ExceptionAsync(() => seasonService.GetSeasons());

            // Assert
            Assert.Null(exception.Result);
        }

        [Fact]
        public void GetSeasonsByShowId_GivenValidShowId_ThenDoesNotThrow()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new List<Season>())
            };

            var seasonService = new SeasonService(new MockConfiguration(), mockHttpClientFactory);

            // Act
            var exception = Record.ExceptionAsync(() => seasonService.GetSeasonsByShowId(1));

            // Assert
            Assert.Null(exception.Result);
        }

        [Fact]
        public void GetSeasonByShowId_GivenShowIdLessThanOne_ThenThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new ArgumentOutOfRangeException())
            };

            var seasonService = new SeasonService(new MockConfiguration(), mockHttpClientFactory);

            // Act
            var exception = Record.ExceptionAsync(() => seasonService.GetSeasonsByShowId(0));

            // Assert
            Assert.IsType<ArgumentOutOfRangeException>(exception.Result.InnerException);
        }

        [Fact]
        public void SaveSeason_GivenVaildSeason_ThenDoesNotThrow()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(1)
            };

            var seasonService = new SeasonService(new MockConfiguration(), mockHttpClientFactory);

            // Act
            var exception = Record.ExceptionAsync(() => seasonService.SaveSeason(new Season()));

            // Assert
            Assert.Null(exception.Result);
        }

        [Fact]
        public void SaveSeason_GivenNullSeason_ThenThrowsArgumentNullException()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new ArgumentNullException())
            };

            var seasonService = new SeasonService(new MockConfiguration(), mockHttpClientFactory);

            // Act
            var exception = Record.ExceptionAsync(() => seasonService.SaveSeason(null));

            // Assert
            Assert.IsType<ArgumentNullException>(exception.Result.InnerException);
        }
    }
}