using FileManager.DataAccessLayer;
using FileManager.DataAccessLayer.Repositories;
using FileManager.Models;
using FileManager.Models.Constants;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xunit;

namespace FileManager.Tests.FileManagerDataAccessLayerTests
{
    [Collection("Database collection")]
    public class EpisodeRepositoryTests
    {
        private readonly FileManagerContext _context;

        public EpisodeRepositoryTests(DatabaseFixture dbFixture)
        {
            _context = dbFixture.Context;
        }

        [Fact]
        public async Task GetEpisodeById_GivenValidId_ThenEpisodeIsReturned()
        {
            // Arrange
            var id = 1;

            var episodeRepo = new EpisodeRepository(_context);

            // Act
            var episode = await episodeRepo.GetEpisodeByIdAsync(id);

            // Assert
            Assert.Equal(id, episode.EpisodeId);
        }

        [Fact]
        public void GetEpisodeByName_GivenValidName_ThenEpisodeIsReturned()
        {
            // Arrange
            var name = "Test";

            var episodeRepo = new EpisodeRepository(_context);

            // Act
            var episode = episodeRepo.GetEpisodeByName(name);

            // Assert
            Assert.IsAssignableFrom<Episode>(episode);
            Assert.Equal(name, episode.Name);
        }

        [Fact]
        public void GetEpisodes_ThenReturnsEpisodeList()
        {
            // Arrange
            var episodeRepo = new EpisodeRepository(_context);

            // Act
            var episodes = episodeRepo.GetEpisodes();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Episode>>(episodes);
        }

        [Fact]
        public void GetEpisodesBySeasonId_GivenValidSeasonId_ThenEpisodesReturned()
        {
            // Arrange
            var id = 1;

            var episodeRepo = new EpisodeRepository(_context);

            // Act
            var episodes = episodeRepo.GetEpisodesBySeasonId(id);

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Episode>>(episodes);
            Assert.NotEmpty(episodes);
        }

        [Fact]
        public async Task SaveEpisode_GivenValidNewEpisode_ThenReturnsId()
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

            var episodeRepo = new EpisodeRepository(_context);

            // Act
            var episodeId = await episodeRepo.SaveEpisodeAsync(episode);

            // Assert
            Assert.True(episodeId > 0);
        }

        [Fact]
        public async Task SaveEpisode_GivenValidExistingEpisode_ThenEpisodeIdIsEqual()
        {
            // Arrange
            var episode = new Episode
            {
                EpisodeId = 1,
                SeasonId = 1,
                Format = FileFormatTypes.MKV,
                Name = "Test Name",
                Path = "Some Path"
            };

            var episodeRepo = new EpisodeRepository(_context);

            // Act
            episode.Name = "Updated Name";

            var episodeId = await episodeRepo.SaveEpisodeAsync(episode);

            // Assert
            Assert.Equal(episode.EpisodeId, episodeId);
        }

        [Fact]
        public async Task SaveEpisode_GivenNonExistingEpisode_ThenThrowsArgumentNullException()
        {
            // Arrange
            var episode = new Episode
            {
                EpisodeId = 10,
                SeasonId = 1,
                Format = FileFormatTypes.MKV,
                Name = "Test Name",
                Path = "Some Path"
            };

            var episodeRepo = new EpisodeRepository(_context);

            // Act
            var exception = await Record.ExceptionAsync(async () => await  episodeRepo.SaveEpisodeAsync(episode));

            // Assert
            Assert.IsType<ArgumentNullException>(exception);
        }
    }
}