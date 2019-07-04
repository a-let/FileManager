using FileManager.Models;
using FileManager.Services;
using FileManager.Tests.Mocks;

using System;
using System.Collections.Generic;

using Xunit;

namespace FileManager.Tests.FileManagerServiceTests
{
    public class EpisodeServiceTests
    {
        [Fact]
        public void GetEpisodes_ThenDoesNotThrow()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new List<Episode>())
            };

            var episodeService = new EpisodeService(new MockConfiguration(), mockHttpClientFactory);

            // Act
            var exception = Record.ExceptionAsync(() => episodeService.GetAsync());

            // Assert
            Assert.Null(exception.Result);
        }

        [Fact]
        public void GetEpisodeById_GivenValidId_ThenDoesNotThrow()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new Episode())
            };

            var episodeService = new EpisodeService(new MockConfiguration(), mockHttpClientFactory);

            // Act
            var exception = Record.ExceptionAsync(() => episodeService.GetAsync(1));

            // Assert
            Assert.Null(exception.Result);
        }

        [Fact]
        public void GetEpisodeById_GivenIdLessThanOne_ThenThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new ArgumentOutOfRangeException())
            };

            var episodeService = new EpisodeService(new MockConfiguration(), mockHttpClientFactory);

            // Act
            var exception = Record.ExceptionAsync(() => episodeService.GetAsync(0));

            // Assert
            Assert.IsType<ArgumentOutOfRangeException>(exception.Result.InnerException);
        }

        [Fact]
        public void GetEpisodeByName_GivenValidName_ThenDoesNotThrow()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new Episode { Name = "Test Name"})
            };

            var episodeService = new EpisodeService(new MockConfiguration(), mockHttpClientFactory);

            // Act
            var exception = Record.ExceptionAsync(() => episodeService.GetAsync("Test Name"));

            // Assert
            Assert.Null(exception.Result);
        }

        [Fact]
        public void GetEpisodeByName_GivenInvalidName_ThenThrowsArgumentNullException()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new ArgumentNullException())
            };

            var episodeService = new EpisodeService(new MockConfiguration(), mockHttpClientFactory);

            // Act
            var exception = Record.ExceptionAsync(() => episodeService.GetAsync(""));

            // Assert
            Assert.IsType<ArgumentNullException>(exception.Result.InnerException);
        }

        [Fact]
        public void SaveEpisode_GivenValidEpisode_ThenDoesNotThrow()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(1)
            };

            var episodeService = new EpisodeService(new MockConfiguration(), mockHttpClientFactory);

            // Act
            var episode = new Episode();

            var exception = Record.ExceptionAsync(() => episodeService.SaveAsync(episode));

            // Assert
            Assert.Null(exception.Result);
        }

        [Fact]
        public void SaveEpisode_GivenNullEpisode_ThenThrowsArgumentNullException()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new ArgumentNullException())
            };

            var episodeService = new EpisodeService(new MockConfiguration(), mockHttpClientFactory);

            // Act
            var exception = Record.ExceptionAsync(() => episodeService.SaveAsync(null));

            // Assert
            Assert.IsType<ArgumentNullException>(exception.Result.InnerException);
        }

        [Fact]
        public void GetEpisodesBySeasonId_GivenValidSeasonId_ThenDoesNotThrow()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new List<Episode>())
            };

            var episodeService = new EpisodeService(new MockConfiguration(), mockHttpClientFactory);

            // Act
            var exception = Record.ExceptionAsync(() => episodeService.GetEpisodesBySeasonId(1));

            // Assert
            Assert.Null(exception.Result);
        }

        [Fact]
        public void GetEpisodesBySeasonId_GivenSeasonIdLessThanOne_ThenThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new ArgumentOutOfRangeException())
            };

            var episodeService = new EpisodeService(new MockConfiguration(), mockHttpClientFactory);

            // Act
            var exception = Record.ExceptionAsync(() => episodeService.GetEpisodesBySeasonId(0));

            // Assert
            Assert.IsType<ArgumentOutOfRangeException>(exception.Result.InnerException);
        }
    }
}