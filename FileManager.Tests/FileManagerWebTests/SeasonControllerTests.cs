using System.Collections.Generic;

using Xunit;

using FileManager.Models;
using FileManager.Tests.Mocks;
using FileManager.Web.Controllers;

namespace FileManager.Tests.FileManagerWebTests
{
    public class SeasonControllerTests
    {
        private readonly SeasonController _seasonController = new SeasonController(new MockSeasonControllerService(), new MockLoggerService());

        [Fact]
        public void Get_GivenNoParameter_ThenReturnsListOfSeasons()
        {
            //Arrange

            //Act
            var seasons = _seasonController.Get();

            //Assert
            Assert.IsAssignableFrom<IEnumerable<Season>>(seasons);
        }

        [Fact]
        public void Get_GivenId_ThenSeasonIsReturned()
        {
            //Arrange
            var id = 1;

            //Act
            var season = _seasonController.Get(id);

            //Assert
            Assert.Equal(id, season.SeasonId);
        }

        [Fact]
        public void Get_GivenSeasonId_ThenReturnsListOfSeasons()
        {
            //Arrange
            var seasonId = 1;

            //Act
            var seasons = _seasonController.GetByShowId(seasonId);

            //Assert
            Assert.IsAssignableFrom<IEnumerable<Season>>(seasons);
        }

        [Fact]
        public void Save_GivenValidSeason_ThenReturnsSeasonId()
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
            var seasonId = _seasonController.Post(season);

            //Assert
            Assert.True(seasonId > 0);
        }
    }
}