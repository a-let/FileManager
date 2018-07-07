using System;

using Xunit;

using FileManager.Models;
using FileManager.Services;
using FileManager.Tests.Mocks;

namespace FileManager.Tests.FileManagerServiceTests
{
    public class EpisodeServiceTests
    {
        private readonly EpisodeService _episodeService = new EpisodeService(new MockConfiguration(), new MockHttpClientFactory());

        [Fact]
        public void GetEpisodes_ThenDoesNotThrow()
        {
            //Arrange, Act
            var exception = Record.Exception(() => _episodeService.GetEpisodes());

            //Assert
            Assert.Null(exception);
        }

        [Fact]
        public void GetEpisodeById_GivenValidId_ThenDoesNotThrow()
        {
            //Arrange, Act
            var exception = Record.Exception(() => _episodeService.GetEpisodeById(1));

            //Assert
            Assert.Null(exception);
        }

        [Fact]
        public void GetEpisodeById_GivenIdLessThanOne_ThenThrowsArgumentOutOfRangeException()
        {
            //Arrange, Act
            var exception = Record.Exception(() => _episodeService.GetEpisodeById(0));

            //Assert
            Assert.IsType<ArgumentOutOfRangeException>(exception.InnerException);
        }

        [Fact]
        public void GetEpisodeByName_GivenValidName_ThenDoesNotThrow()
        {
            //Arrange, Act
            var exception = Record.Exception(() => _episodeService.GetEpisodeByName("Test Name"));

            //Assert
            Assert.Null(exception);
        }

        [Fact]
        public void GetEpisodeByName_GivenInvalidName_ThenThrowsArgumentNullException()
        {
            //Arrange, Act
            var exception = Record.Exception(() => _episodeService.GetEpisodeByName(""));

            //Assert
            Assert.IsType<ArgumentNullException>(exception.InnerException);
        }

        [Fact]
        public void SaveEpisode_GivenValidEpisode_ThenDoesNotThrow()
        {
            //Arrange, Act
            var episode = new Episode();

            var exception = Record.Exception(() => _episodeService.SaveEpisode(episode));

            //Assert
            Assert.Null(exception);
        }

        [Fact]
        public void SaveEpisode_GivenNullEpisode_ThenThrowsArgumentNullException()
        {
            //Arrange, Act
            var exception = Record.Exception(() => _episodeService.SaveEpisode(null));

            //Assert
            Assert.IsType<ArgumentNullException>(exception.InnerException);
        }

        [Fact]
        public void GetEpisodesBySeasonId_GivenValidSeasonId_ThenDoesNotThrow()
        {
            //Arrange, Act
            var exception = Record.Exception(() => _episodeService.GetEpisodesBySeasonId(1));

            //Assert
            Assert.Null(exception);
        }

        [Fact]
        public void GetEpisodesBySeasonId_GivenSeasonIdLessThanOne_ThenThrowsArgumentOutOfRangeException()
        {
            //Arrange, Act
            var exception = Record.Exception(() => _episodeService.GetEpisodesBySeasonId(0));

            //Assert
            Assert.IsType<ArgumentOutOfRangeException>(exception.InnerException);
        }
    }
}