using FileManager.Models.Constants;
using FileManager.Models;
using FileManager.Tests.Mocks;
using FileManager.Web.Services;

using System;
using System.Collections.Generic;

using Xunit;
using System.Threading.Tasks;

namespace FileManager.Tests.FileManagerWebTests
{
    public class EpisodeControllerServiceTests
    {
        private readonly EpisodeControllerService _episodeControllerService = new EpisodeControllerService(new MockEpisodeRepository());

        [Fact]
        public async Task GetEpisodeById_GivenInvalidId_ThenThrowsArgumentException()
        {
            // Arrange
            var id = 0;

            // Act
            var exception = await Record.ExceptionAsync(async () => await _episodeControllerService.GetAsync(id));

            // Assert
            Assert.IsType<ArgumentException>(exception);
        }

        [Fact]
        public async Task GetEpisodeById_GivenValidId_ThenEpisodeIsReturned()
        {
            // Arrange
            var id = 1;

            // Act
            var episode = await _episodeControllerService.GetAsync(id);

            // Assert
            Assert.IsAssignableFrom<Episode>(episode);
        }

        [Fact]
        public async Task GetEpisodes_ThenReturnsEpisodeList()
        {
            // Arrange, Act
            var episodes = await _episodeControllerService.GetAsync();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Episode>>(episodes);
        }

        [Fact]
        public async Task GetEpisodeByName_GivenInvalidName_ThenThrowsArgumentNullException()
        {
            // Arrange
            var name = string.Empty;

            // Act
            var exception = await Record.ExceptionAsync(async () => await _episodeControllerService.GetAsync(name));

            // Assert
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task GetEpisodeByName_GivenValidName_ThenEpisodeIsReturned()
        {
            // Arrange
            var name = "Test";

            // Act
            var episode = await _episodeControllerService.GetAsync(name);

            // Assert
            Assert.IsAssignableFrom<Episode>(episode);
        }

        [Fact]
        public async Task SaveEpisode_GivenNullEpisode_ThenThrowsArgumentNullReferenceException()
        {
            // Arrange
            Episode episode = null;

            // Act
            var exception = await Record.ExceptionAsync(async () => await _episodeControllerService.SaveAsync(episode));

            // Assert
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task SaveEpisode_GivenNewEpisode_ThenReturnsOne()
        {
            // Arrange
            var episode = new Episode
            {
                EpisodeId = 0,
                SeasonId = 1,
                Format = FileFormatTypes.MKV,
                Name = "Test Name",
                Path = "Some Path"
            };

            // Act
            var episodeId = await _episodeControllerService.SaveAsync(episode);

            // Assert
            Assert.Equal(1, episodeId);
        }

        [Fact]
        public void GetEpisodesBySeasonId_GivenInvalidSeasonId_ThenThrowsArgumentException()
        {
            // Arrange
            var id = 0;

            // Act
            var exception = Record.Exception(() => _episodeControllerService.GetEpisodesBySeasonId(id));

            // Assert
            Assert.IsType<ArgumentException>(exception);
        }

        [Fact]
        public void GetEpisodesBySeasonId_GivenValidSeasonId_THenThrowsArgumentException()
        {
            // Arrange
            var id = 1;

            // Act
            var episodes = _episodeControllerService.GetEpisodesBySeasonId(id);

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Episode>>(episodes);
        }
    }
}