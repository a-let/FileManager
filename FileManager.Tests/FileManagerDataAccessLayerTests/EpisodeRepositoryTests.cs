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
        private readonly EpisodeRepository _episodeRepository;

        public EpisodeRepositoryTests(DatabaseFixture dbFixture)
        {
            _episodeRepository = dbFixture.EpisodeRepo;
        }

        [Fact]
        public async Task GetEpisodeById_GivenValidId_ThenEpisodeIsReturned()
        {
            //Arrange
            var id = 1;

            //Act
            var episode = await _episodeRepository.GetEpisodeByIdAsync(id);

            //Assert
            Assert.Equal(id, episode.EpisodeId);
        }

        [Fact]
        public void GetEpisodeByName_GivenValidName_ThenEpisodeIsReturned()
        {
            //Arrange
            var name = "Test";

            //Act
            var episode = _episodeRepository.GetEpisodeByName(name);

            //Assert
            Assert.IsAssignableFrom<Episode>(episode);
            Assert.Equal(name, episode.Name);
        }

        [Fact]
        public void GetEpisodes_ThenReturnsEpisodeList()
        {
            //Arrange, Act
            var episodes = _episodeRepository.GetEpisodes();

            //Assert
            Assert.IsAssignableFrom<IEnumerable<Episode>>(episodes);
        }

        [Fact]
        public void GetEpisodesBySeasonId_GivenValidSeasonId_ThenEpisodesReturned()
        {
            //Arrange
            var id = 1;

            //Act
            var episodes = _episodeRepository.GetEpisodesBySeasonId(id);

            //Assert
            Assert.IsAssignableFrom<IEnumerable<Episode>>(episodes);
            Assert.NotEmpty(episodes);
        }

        [Fact]
        public async Task SaveEpisode_GivenValidNewEpisode_ThenReturnsId()
        {
            //Arrange
            var episode = new Episode
            {
                EpisodeId = 0,
                SeasonId = 1,
                Format = FileFormatTypes.MKV,
                Name = "Test Name",
                Path = "Some Path"
            };

            //Act
            var episodeId = await _episodeRepository.SaveEpisodeAsync(episode);

            //Assert
            Assert.True(episodeId > 0);
        }

        [Fact(Skip = "Test is just saving a new record. Fix after DAL refactor.")]
        public async Task SaveEpisode_GivenValidExistingEpisode_ThenEpisodeIdIsEqual()
        {
            //Arrange
            var episode = new Episode
            {
                EpisodeId = 0,
                SeasonId = 1,
                Format = FileFormatTypes.MKV,
                Name = "Test Name",
                Path = "Some Path"
            };

            //Act
            episode.Name = "Updated Name";

            var episodeId = await _episodeRepository.SaveEpisodeAsync(episode);

            //Assert
            Assert.Equal(episode.EpisodeId, episodeId);
        }

        [Fact]
        public async Task SaveEpisode_GivenNonExistingEpisode_ThenThrowsArgumentNullException()
        {
            //Arrange
            var episode = new Episode
            {
                EpisodeId = 10,
                SeasonId = 1,
                Format = FileFormatTypes.MKV,
                Name = "Test Name",
                Path = "Some Path"
            };

            //Act
            var exception = await Record.ExceptionAsync(async () => await  _episodeRepository.SaveEpisodeAsync(episode));

            //Assert
            Assert.IsType<ArgumentNullException>(exception);
        }
    }
}