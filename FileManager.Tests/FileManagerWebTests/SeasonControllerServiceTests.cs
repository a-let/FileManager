using FileManager.Models;
using FileManager.Tests.Mocks;
using FileManager.Web.Services;

using System;
using System.Collections.Generic;

using Xunit;

namespace FileManager.Tests.FileManagerWebTests
{
    public class SeasonControllerServiceTests
    {
        private readonly SeasonControllerService _seasonControllerService = new SeasonControllerService(new MockSeasonRepository());

        [Fact]
        public void GetSeasonById_GivenInvalidId_ThenThrowsArgumentException()
        {
            //Arrange
            var id = 0;

            //Act
            var exception = Record.Exception(() => _seasonControllerService.GetSeasonById(id));

            //Assert
            Assert.IsType<ArgumentException>(exception);
        }

        [Fact]
        public void GetSeasonById_GivenValidId_ThenSeasonIsReturned()
        {
            //Arrange
            var id = 1;

            //Act
            var season = _seasonControllerService.GetSeasonById(id);

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
        public void SaveSeason_GivenNullSeason_ThenThrowsArgumentNullReferenceException()
        {
            //Arrange
            Season season = null;

            //Act
            var exception = Record.Exception(() => _seasonControllerService.SaveSeason(season));

            //Assert
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void SaveSeason_GivenSeason_ThenReturnsTrue()
        {
            //Arrange
            var season = new Season
            {

            };

            //Act
            var success = _seasonControllerService.SaveSeason(season);

            //Assert
            Assert.True(success);
        }
    }
}