using Xunit;

using FileManager.BusinessLayer.Repositories;
using FileManager.Models;
using FileManager.Tests.Mocks;

namespace FileManager.Tests.FileManagerBusinessLayerTests
{
    public class SeasonRepositoryTests
    {
        private readonly SeasonRepository _seasonRepository = new SeasonRepository(new MockFileManagerDb(typeof(Season)), new EpisodeRepository(new MockFileManagerDb(typeof(Episode))));

        [Fact]
        public void GetById_GivenValidId_ThenSeasonIsNotNull()
        {
            //Arrange
            var id = 1;

            //Act
            var season = _seasonRepository.GetById(id);

            //Assert
            Assert.NotNull(season);
        }

        [Fact]
        public void Get_ThenSeasonListIsNotEmpty()
        {
            //Arrange, Act
            var seasonList = _seasonRepository.Get();

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
            var success = _seasonRepository.Save(season);

            //Assert
            Assert.True(success);
        }

        [Fact]
        public void Save_GivenInvalidSeason_ThenReturnsFalse()
        {
            //Arrange
            var season = new Season();

            //Act
            var success = _seasonRepository.Save(season);

            //Assert
            Assert.False(success);
        }

        [Fact]
        public void GetByParentId_GivenValidId_ThenSeasonListIsNotEmpty()
        {
            //Arrange
            var id = 1;

            //Act
            var seasonList = _seasonRepository.GetByParentId(id);

            //Assert
            Assert.NotEmpty(seasonList);
        }
    }
}