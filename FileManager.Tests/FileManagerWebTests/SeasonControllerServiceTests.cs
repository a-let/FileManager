using FileManager.Models;
using FileManager.Tests.Mocks;
using FileManager.Web.Services;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xunit;

namespace FileManager.Tests.FileManagerWebTests
{
    public class SeasonControllerServiceTests
    {
        private readonly SeasonControllerService _seasonControllerService = new SeasonControllerService(new MockSeasonRepository());

        [Fact]
        public async Task GetSeasonById_GivenInvalidId_ThenThrowsArgumentException()
        {
            //Arrange
            var id = 0;

            //Act
            var exception = await Record.ExceptionAsync(async () => await _seasonControllerService.GetSeasonByIdAsync(id));

            //Assert
            Assert.IsType<ArgumentException>(exception);
        }

        [Fact]
        public async Task GetSeasonById_GivenValidId_ThenSeasonIsReturned()
        {
            //Arrange
            var id = 1;

            //Act
            var season = await _seasonControllerService.GetSeasonByIdAsync(id);

            //Assert
            Assert.IsAssignableFrom<Season>(season);
        }

        [Fact]
        public void GetSeasonByShowId_GivenValidId_ThenSeasonListIsReturned()
        {
            //Arrange
            var id = 1;

            //Act
            var seasons = _seasonControllerService.GetSeasonsByShowId(id);

            //Assert
            Assert.IsAssignableFrom<IEnumerable<Season>>(seasons);
        }

        [Fact]
        public void GetSeasonByShowId_GivenInvalidId_ThenThrowsArgumentException()
        {
            //Arrange
            var id = 0;

            //Act
            var exception = Record.Exception(() => _seasonControllerService.GetSeasonsByShowId(id));

            //Assert
            Assert.IsType<ArgumentException>(exception);
        }

        [Fact]
        public void GetSeasons_ThenReturnsSeasonList()
        {
            //Arrange, Act
            var seasons = _seasonControllerService.GetSeasons();

            //Assert
            Assert.IsAssignableFrom<IEnumerable<Season>>(seasons);
        }

        [Fact]
        public async Task SaveSeason_GivenNullSeason_ThenThrowsArgumentNullReferenceException()
        {
            //Arrange
            Season season = null;

            //Act
            var exception = await Record.ExceptionAsync(async () =>  await _seasonControllerService.SaveSeasonAsync(season));

            //Assert
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task SaveSeason_GivenSeason_ThenReturnsSeasonId()
        {
            //Arrange
            var season = new Season
            {

            };

            //Act
            var seasonId = await _seasonControllerService.SaveSeasonAsync(season);

            //Assert
            Assert.True(seasonId > 0);
        }
    }
}