using Xunit;

using FileManager.BusinessLayer.Adapters;
using FileManager.BusinessLayer.Interfaces;
using FileManager.Models;
using FileManager.Tests.Mocks;

namespace FileManager.Tests.FileManagerBusinessLayerTests
{
    public class EpisodeAdapterTests
    {
        private IFileManagerObjectAdapter<Episode> _episodeAdapter = new EpisodeAdapter(new MockFileManagerDb(typeof(Episode)));

        [Fact]
        public void GetById_GivenValidId_ThenEpisodeIsNotNull()
        {
            //Arrange
            var id = 1;

            //Act
            var episode = _episodeAdapter.GetById(id);

            //Assert
            Assert.NotNull(episode);
        }

        [Fact]
        public void Get_ThenEpisodeListIsNotEmpty()
        {
            //Arrange
            //Act
            var episodeList = _episodeAdapter.Get();

            //Assert
            Assert.NotEmpty(episodeList);
        }

        [Fact]
        public void GetByName_GivenValidName_ThenEpisodeIsNotNull()
        {
            //Arrange
            var name = "Test Name";

            //Act
            var episode = _episodeAdapter.GetByName(name);

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
                Format = "Test",
                Path = "Test"
            };

            //Act
            var success = _episodeAdapter.Save(episode);

            //Assert
            Assert.True(success);
        }

        [Fact]
        public void GetByParentId_GivenValidId_ThenEpisodeListIsNotEmpty()
        {
            //Arrange
            var id = 1;

            //Act
            var episodeList = _episodeAdapter.GetByParentId(id);

            //Assert
            Assert.NotEmpty(episodeList);
        }
    }
}