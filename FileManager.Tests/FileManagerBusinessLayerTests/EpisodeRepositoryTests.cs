using Xunit;

using FileManager.BusinessLayer.Repositories;
using FileManager.Models;
using FileManager.Tests.Mocks;
using FileManager.Models.Constants;

namespace FileManager.Tests.FileManagerBusinessLayerTests
{
    public class EpisodeRepositoryTests
    {
        private readonly EpisodeRepository _episodeRepository = new EpisodeRepository(new MockFileManagerDb(typeof(Episode)));

        [Fact]
        public void GetById_GivenValidId_ThenEpisodeIsNotNull()
        {
            //Arrange
            var id = 1;

            //Act
            var episode = _episodeRepository.GetById(id);

            //Assert
            Assert.NotNull(episode);
        }

        [Fact]
        public void Get_ThenEpisodeListIsNotEmpty()
        {
            //Arrange, Act
            var episodeList = _episodeRepository.Get();

            //Assert
            Assert.NotEmpty(episodeList);
        }

        [Fact]
        public void GetByName_GivenValidName_ThenEpisodeIsNotNull()
        {
            //Arrange
            var name = "Test Name";

            //Act
            var episode = _episodeRepository.GetByName(name);

            //Assert
            Assert.NotNull(episode);
        }

        [Fact]
        public void Save_GivenValidEpisode_ThenReturnsTrue()
        {
            //Arrange
            var episode = new Episode
            {
                EpisodeId = 1,
                SeasonId = 1,
                Name = "Test",
                EpisodeNumber = 1,
                Format = FileFormatTypes.MP4,
                Path = "Test"
            };

            //Act
            var success = _episodeRepository.Save(episode);

            //Assert
            Assert.True(success);
        }

        [Fact]
        public void Save_GivenInvalidEpisode_ThenReturnsFalse()
        {
            //Arrange
            var episode = new Episode
            {
                EpisodeNumber = 1,
                Format = FileFormatTypes.MP4,
                Path = "Test"
            };

            //Act
            var success = _episodeRepository.Save(episode);

            //Assert
            Assert.False(success);
        }

        [Fact]
        public void GetByParentId_GivenValidId_ThenEpisodeListIsNotEmpty()
        {
            //Arrange
            var id = 1;

            //Act
            var episodeList = _episodeRepository.GetByParentId(id);

            //Assert
            Assert.NotEmpty(episodeList);
        }
    }
}