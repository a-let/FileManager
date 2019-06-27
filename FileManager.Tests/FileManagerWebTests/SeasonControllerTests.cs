using FileManager.Models;
using FileManager.Tests.Mocks;
using FileManager.Web.Controllers;

using System.Collections.Generic;
using System.Threading.Tasks;

using Xunit;

namespace FileManager.Tests.FileManagerWebTests
{
    public class SeasonControllerTests
    {
        private readonly SeasonController _seasonController = new SeasonController(new MockSeasonControllerService(), new MockLog());

        [Fact]
        public async Task Get_GivenNoParameter_ThenReturnsListOfSeasons()
        {
            // Arrange

            // Act
            var seasons = (await _seasonController.Get()).GetValue();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Season>>(seasons);
        }

        [Fact]
        public async Task Get_GivenId_ThenSeasonIsReturned()
        {
            // Arrange
            var id = 1;

            // Act
            var season = (await _seasonController.GetById(id)).GetValue();

            // Assert
            Assert.Equal(id, season.SeasonId);
        }

        [Fact]
        public async Task Get_GivenSeasonId_ThenReturnsListOfSeasons()
        {
            // Arrange
            var seasonId = 1;

            // Act
            var seasons = (await _seasonController.GetByShowId(seasonId)).GetValue();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Season>>(seasons);
        }

        [Fact]
        public async Task Save_GivenValidSeason_ThenReturnsSeasonId()
        {
            // Arrange
            var season = new Season
            {
                SeasonId = 1,
                ShowId = 1,
                SeasonNumber = 1,
                Path = "Test"
            };

            // Act
            var seasonId = (await _seasonController.Post(season)).GetValue();

            // Assert
            Assert.True(seasonId > 0);
        }
    }
}