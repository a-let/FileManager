using FileManager.Models;
using FileManager.Services;
using FileManager.Tests.Mocks;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xunit;

namespace FileManager.Tests.FileManagerServiceTests
{
    public class SeasonServiceTests
    {
        [Fact]
        public async Task GetBySeasonId_GivenValidSeasonId_ThenDoesNotThrow()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new Season())
            };

            var seasonService = new SeasonService(new MockConfiguration(), mockHttpClientFactory, new MockLog());

            // Act
            var exception = await Record.ExceptionAsync(async () => await seasonService.GetAsync(1));

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public async Task GetBySeasonId_GivenIdLessThanOne_ThenThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new ArgumentOutOfRangeException())
            };

            var seasonService = new SeasonService(new MockConfiguration(), mockHttpClientFactory, new MockLog());

            // Act
            var exception = await Record.ExceptionAsync(async () => await seasonService.GetAsync(0));

            // Assert
            Assert.IsType<ArgumentOutOfRangeException>(exception);
        }

        [Fact]
        public async Task GetSeasons_ThenDoesNotThrow()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new List<Season>())
            };

            var seasonService = new SeasonService(new MockConfiguration(), mockHttpClientFactory, new MockLog());

            // Act
            var exception = await Record.ExceptionAsync(async () => await seasonService.GetAsync());

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public async Task GetSeasonsByShowId_GivenValidShowId_ThenDoesNotThrow()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new List<Season>())
            };

            var seasonService = new SeasonService(new MockConfiguration(), mockHttpClientFactory, new MockLog());

            // Act
            var exception = await Record.ExceptionAsync(async () => await seasonService.GetSeasonsByShowId(1));

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public async Task GetSeasonByShowId_GivenShowIdLessThanOne_ThenThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new ArgumentOutOfRangeException())
            };

            var seasonService = new SeasonService(new MockConfiguration(), mockHttpClientFactory, new MockLog());

            // Act
            var exception = await Record.ExceptionAsync(async () => await seasonService.GetSeasonsByShowId(0));

            // Assert
            Assert.IsType<ArgumentOutOfRangeException>(exception);
        }

        [Fact]
        public async Task SaveSeason_GivenVaildSeason_ThenDoesNotThrow()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(1)
            };

            var seasonService = new SeasonService(new MockConfiguration(), mockHttpClientFactory, new MockLog());

            // Act
            var exception = await Record.ExceptionAsync(async () => await seasonService.SaveAsync(new Season()));

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public async Task SaveSeason_GivenNullSeason_ThenThrowsArgumentNullException()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new ArgumentNullException())
            };

            var seasonService = new SeasonService(new MockConfiguration(), mockHttpClientFactory, new MockLog());

            // Act
            var exception = await Record.ExceptionAsync(async () => await seasonService.SaveAsync(null));

            // Assert
            Assert.IsType<ArgumentNullException>(exception);
        }
    }
}