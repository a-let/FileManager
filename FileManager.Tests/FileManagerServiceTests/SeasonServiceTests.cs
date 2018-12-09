using System;

using Xunit;

using FileManager.Models;
using FileManager.Services;
using FileManager.Tests.Mocks;

namespace FileManager.Tests.FileManagerServiceTests
{
    public class SeasonServiceTests
    {
        //private readonly SeasonService _seasonService = new SeasonService(new MockConfiguration(), new MockHttpClientFactory());
        private readonly SeasonService _seasonService = new SeasonService(new MockConfiguration(), null);

        [Fact]
        public void GetBySeasonId_GivenValidSeasonId_ThenDoesNotThrow()
        {
            //Arrange
            var id = 1;

            //Act
            var exception = Record.Exception(() => _seasonService.GetSeasonById(id));

            //Assert
            Assert.Null(exception);
        }

        [Fact]
        public void GetBySeasonId_GivenIdLessThanOne_ThenThrowsArgumentOutOfRangeException()
        {
            //Arrange
            var id = 0;

            //Act
            var exception = Record.Exception(() => _seasonService.GetSeasonById(id));

            //Assert
            Assert.IsType<ArgumentOutOfRangeException>(exception.InnerException);
        }

        [Fact]
        public void GetSeasons_ThenDoesNotThrow()
        {
            //Arrange, Act
            var exception = Record.Exception(() => _seasonService.GetSeasons());

            //Assert
            Assert.Null(exception);
        }

        [Fact]
        public void GetSeasonsByShowId_GivenValidShowId_ThenDoesNotThrow()
        {
            //Arrange
            var id = 1;

            //Act
            var exception = Record.Exception(() => _seasonService.GetSeasonsByShowId(id));

            //Assert
            Assert.Null(exception);
        }

        [Fact]
        public void GetSeasonByShowId_GivenShowIdLessThanOne_ThenThrowsArgumentOutOfRangeException()
        {
            //Arrange
            var id = -1;

            //Act
            var exception = Record.Exception(() => _seasonService.GetSeasonsByShowId(id));

            //Assert
            Assert.IsType<ArgumentOutOfRangeException>(exception.InnerException);
        }

        [Fact]
        public void SaveSeason_GivenVaildSeason_ThenDoesNotThrow()
        {
            //Arrange
            var season = new Season();

            //Act
            var exception = Record.Exception(() => _seasonService.SaveSeason(season));

            //Assert
            Assert.Null(exception);
        }

        [Fact]
        public void SaveSeason_GivenNullSeason_ThenThrowsArgumentNullException()
        {
            //Arrange, Act
            var exception = Record.Exception(() => _seasonService.SaveSeason(null));

            //Assert
            Assert.IsType<ArgumentNullException>(exception.InnerException);
        }
    }
}