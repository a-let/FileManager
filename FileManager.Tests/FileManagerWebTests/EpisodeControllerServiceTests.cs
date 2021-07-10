using FileManager.DataAccessLayer.Interfaces;
using FileManager.Models;
using FileManager.Models.Constants;
using FileManager.Web.Services;

using NSubstitute;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xunit;

namespace FileManager.Tests.FileManagerWebTests
{
    public class EpisodeControllerServiceTests
    {
        private readonly IRepository<Episode> _episodeRepo;
        private readonly EpisodeControllerService _episodeControllerService;

        public EpisodeControllerServiceTests()
        {
            _episodeRepo = Substitute.For<IRepository<Episode>>();
            _episodeControllerService = new EpisodeControllerService(_episodeRepo);
        }

        [Fact]
        public async Task GetEpisodeById_GivenInvalidId_ThenThrowsArgumentException()
        {
            // Arrange
            var id = 0;

            // Act
            var exception = await Record.ExceptionAsync(async () => await _episodeControllerService.GetByIdAsync(id));

            // Assert
            Assert.IsType<ArgumentException>(exception);
        }

        [Fact]
        public async Task GetEpisodeById_GivenValidId_ThenEpisodeIsReturned()
        {
            // Arrange
            var id = 1;

            _episodeRepo.GetByIdAsync(Arg.Any<int>())
                .Returns(new Episode());

            // Act
            var episode = await _episodeControllerService.GetByIdAsync(id);

            // Assert
            Assert.IsAssignableFrom<Episode>(episode);
        }

        [Fact]
        public void GetEpisodes_ThenReturnsEpisodeList()
        {
            // Arrange
            _episodeRepo.Get()
                .Returns(new List<Episode>());

            // Act
            var episodes = _episodeControllerService.Get();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Episode>>(episodes);
        }

        [Fact]
        public void GetEpisodeByName_GivenInvalidName_ThenThrowsArgumentNullException()
        {
            // Arrange
            var name = string.Empty;

            // Act
            var exception = Record.Exception(() => _episodeControllerService.GetByName(name));

            // Assert
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void GetEpisodeByName_GivenValidName_ThenEpisodeIsReturned()
        {
            // Arrange
            var name = "Test";

            _episodeRepo.GetByName(Arg.Any<string>())
                .Returns(new Episode());

            // Act
            var episode = _episodeControllerService.GetByName(name);

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
            var exception = await Record.ExceptionAsync(async () => await _episodeControllerService.SaveAsync(episode));

            // Assert
            Assert.Null(exception);
        }
    }
}