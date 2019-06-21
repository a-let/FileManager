using FileManager.DataAccessLayer.Repositories;
using FileManager.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;

namespace FileManager.Tests.FileManagerDataAccessLayerTests
{
    [Collection("Database collection")]
    public class SeasonRepositoryTests
    {
        private readonly SeasonRepository _seasonRepository;

        public SeasonRepositoryTests(DatabaseFixture dbFixture)
        {
            _seasonRepository = dbFixture.SeasonRepository;
        }

        [Fact]
        public async Task GetSeasonById_GivenValidId_ThenSeasonIsReturned()
        {
            //Arrange
            var id = 1;

            //Act
            var season = await _seasonRepository.GetSeasonByIdAsync(id);

            //Assert
            Assert.Equal(id, season.SeasonId);
        }

        [Fact]
        public void GetSeasons_ThenReturnsSeasonList()
        {
            //Arrange, Act
            var seasons = _seasonRepository.GetSeasons();

            //Assert
            Assert.IsAssignableFrom<IEnumerable<Season>>(seasons);
        }

        [Fact]
        public void GetSeasonsByShowId_GivenValidShowId_ThenSeasonsReturned()
        {
            //Arrange
            var id = 1;

            //Act
            var seasons = _seasonRepository.GetSeasonsByShowId(id);

            //Assert
            Assert.IsAssignableFrom<IEnumerable<Season>>(seasons);
            Assert.True(seasons.Count() > 0);
        }

        [Fact]
        public async Task SaveSeason_GivenValidNewSeason_ThenReturnsSeasonId()
        {
            //Arrange
            var season = new Season
            {
                SeasonId = 0,
                ShowId = 1,
                SeasonNumber = 1,
                Path = "Test"
            };

            //Act
            var seasonId = await _seasonRepository.SaveSeasonAsync(season);

            //Assert
            Assert.True(seasonId > 0);
        }

        [Fact(Skip = "Test is just saving a new record. Fix after DAL refactor.")]
        public async Task SaveSeason_GivenValidExistingSeason_ThenSeasonIdIsEqual()
        {
            //Arrange
            var season = new Season
            {
                SeasonId = 0,
                ShowId = 1,
                SeasonNumber = 1,
                Path = "Test"
            };

            //Act
            season.Path = "Updated Path";

            var seasonId = await _seasonRepository.SaveSeasonAsync(season);

            //Assert
            Assert.Equal(season.SeasonId, seasonId);
        }

        [Fact]
        public async Task SaveSeason_GivenNonExistingSeason_ThenThrowsArgumentNullException()
        {
            //Arrange
            var season = new Season
            {
                SeasonId = 10,
                ShowId = 1,
                SeasonNumber = 1,
                Path = "Test"
            };

            //Act
            var exception = await Record.ExceptionAsync(async () => await _seasonRepository.SaveSeasonAsync(season));

            //Assert
            Assert.IsType<ArgumentNullException>(exception);
        }
    }
}