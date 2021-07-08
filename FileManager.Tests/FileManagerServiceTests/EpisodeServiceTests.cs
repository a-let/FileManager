using FileManager.Models;
using FileManager.Services;
using FileManager.Tests.Mocks;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xunit;

namespace FileManager.Tests.FileManagerServiceTests
{
    public class EpisodeServiceTests
    {
        [Fact]
        public async Task GetEpisodes_ThenDoesNotThrow()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new List<Episode>())
            };

            var episodeService = new EpisodeService(new MockConfiguration(), mockHttpClientFactory, new MockLog<EpisodeService>());

            // Act
            var exception = await Record.ExceptionAsync(async () => await episodeService.GetAsync());

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public async Task GetEpisodeById_GivenValidId_ThenDoesNotThrow()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new Episode())
            };

            var episodeService = new EpisodeService(new MockConfiguration(), mockHttpClientFactory, new MockLog<EpisodeService>());

            // Act
            var exception = await Record.ExceptionAsync(async () => await episodeService.GetAsync(1));

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public async Task GetEpisodeById_GivenIdLessThanOne_ThenThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new ArgumentOutOfRangeException())
            };

            var episodeService = new EpisodeService(new MockConfiguration(), mockHttpClientFactory, new MockLog<EpisodeService>());

            // Act
            var exception = await Record.ExceptionAsync(async () => await episodeService.GetAsync(0));

            // Assert
            Assert.IsType<ArgumentOutOfRangeException>(exception);
        }

        [Fact]
        public async Task GetEpisodeByName_GivenValidName_ThenDoesNotThrow()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new Episode { Name = "Test Name"})
            };

            var episodeService = new EpisodeService(new MockConfiguration(), mockHttpClientFactory, new MockLog<EpisodeService>());

            // Act
            var exception = await Record.ExceptionAsync(async () => await episodeService.GetAsync("Test Name"));

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public async Task GetEpisodeByName_GivenInvalidName_ThenThrowsArgumentNullException()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new ArgumentNullException())
            };

            var episodeService = new EpisodeService(new MockConfiguration(), mockHttpClientFactory, new MockLog<EpisodeService>());

            // Act
            var exception = await Record.ExceptionAsync(async () => await episodeService.GetAsync(""));

            // Assert
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task SaveEpisode_GivenValidEpisode_ThenDoesNotThrow()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(1)
            };

            var episodeService = new EpisodeService(new MockConfiguration(), mockHttpClientFactory, new MockLog<EpisodeService>());

            // Act
            var episode = new Episode();

            var exception = await Record.ExceptionAsync(async () => await episodeService.SaveAsync(episode));

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public async Task SaveEpisode_GivenNullEpisode_ThenThrowsArgumentNullException()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new ArgumentNullException())
            };

            var episodeService = new EpisodeService(new MockConfiguration(), mockHttpClientFactory, new MockLog<EpisodeService>());

            // Act
            var exception = await Record.ExceptionAsync(async () => await episodeService.SaveAsync(null));

            // Assert
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task GetEpisodesBySeasonId_GivenValidSeasonId_ThenDoesNotThrow()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new List<Episode>())
            };

            var episodeService = new EpisodeService(new MockConfiguration(), mockHttpClientFactory, new MockLog<EpisodeService>());

            // Act
            var exception = await Record.ExceptionAsync(async () => await episodeService.GetEpisodesBySeasonId(1));

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public async Task GetEpisodesBySeasonId_GivenSeasonIdLessThanOne_ThenThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new ArgumentOutOfRangeException())
            };

            var episodeService = new EpisodeService(new MockConfiguration(), mockHttpClientFactory, new MockLog<EpisodeService>());

            // Act
            var exception = await Record.ExceptionAsync(async () => await episodeService.GetEpisodesBySeasonId(0));

            // Assert
            Assert.IsType<ArgumentOutOfRangeException>(exception);
        }
    }
}