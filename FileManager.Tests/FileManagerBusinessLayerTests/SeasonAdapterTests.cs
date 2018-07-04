using Xunit;

using FileManager.BusinessLayer.Adapters;
using FileManager.BusinessLayer.Interfaces;
using FileManager.Models;
using FileManager.Tests.Mocks;

namespace FileManager.Tests.FileManagerBusinessLayerTests
{
    public class SeasonAdapterTests
    {
        private readonly IFileManagerObjectAdapter<Season> _seasonAdapter = new SeasonAdapter(new MockFileManagerDb(typeof(Season)), new EpisodeAdapter(new MockFileManagerDb(typeof(Episode))));

        [Fact]
        public void GetById_GivenValidId_ThenSeasonIsNotNull()
        {
            //Arrange
            var id = 1;

            //Act
            var season = _seasonAdapter.GetById(id);

            //Assert
            Assert.NotNull(season);
        }

        [Fact]
        public void Get_ThenSeasonListIsNotEmpty()
        {
            //Arrange, Act
            var seasonList = _seasonAdapter.Get();

            //Assert
            Assert.NotEmpty(seasonList);
        }

        [Fact]
        public void Save_GivenValidSeason_ThenReturnsTrue()
        {
            //Arrange
            var season = new Season
            {
                SeasonId = 1,
                ShowId = 1,
                SeasonNumber = 1,
                Path = "Test"
            };

            //Act
            var success = _seasonAdapter.Save(season);

            //Assert
            Assert.True(success);
        }

        [Fact]
        public void Save_GivenInvalidSeason_ThenReturnsFalse()
        {
            //Arrange
            var season = new Season();

            //Act
            var success = _seasonAdapter.Save(season);

            //Assert
            Assert.False(success);
        }

        [Fact]
        public void GetByParentId_GivenValidId_ThenSeasonListIsNotEmpty()
        {
            //Arrange
            var id = 1;

            //Act
            var seasonList = _seasonAdapter.GetByParentId(id);

            //Assert
            Assert.NotEmpty(seasonList);
        }
    }
}