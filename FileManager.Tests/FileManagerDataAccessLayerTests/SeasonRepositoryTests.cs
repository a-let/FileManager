using FileManager.DataAccessLayer.Repositories;
using FileManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;

using Xunit;

namespace FileManager.Tests.FileManagerDataAccessLayerTests
{
    public class SeasonRepositoryTests : TestBase
    {
        private readonly SeasonRepository _seasonRepository;

        public SeasonRepositoryTests() : base(nameof(SeasonRepositoryTests))
        {
            _seasonRepository = new SeasonRepository(_context);
        }

        [Fact]
        public void GetSeasonById_GivenValidId_ThenSeasonIsReturned()
        {
            //Arrange
            var id = 1;

            _context.Season.Add(new Season
            {
                SeasonId = 1,
                ShowId = 1,
                SeasonNumber = 1,
                Path = "Test"
            });

            _context.SaveChanges();

            //Act
            var season = _seasonRepository.GetSeasonById(id);

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

            _context.Season.Add(new Season
            {
                SeasonId = 1,
                ShowId = id,
                SeasonNumber = 1,
                Path = "Test"
            });

            _context.SaveChanges();

            //Act
            var seasons = _seasonRepository.GetSeasonsByShowId(id);

            //Assert
            Assert.IsAssignableFrom<IEnumerable<Season>>(seasons);
            Assert.True(seasons.Count() == 1);
        }

        [Fact]
        public void SaveSeason_GivenValidNewSeason_ThenReturnsSeasonId()
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
            var seasonId = _seasonRepository.SaveSeason(season);

            //Assert
            Assert.True(seasonId > 0);
        }

        [Fact]
        public void SaveSeason_GivenValidExistingSeason_ThenSeasonIdIsEqual()
        {
            //Arrange
            var season = new Season
            {
                SeasonId = 0,
                ShowId = 1,
                SeasonNumber = 1,
                Path = "Test"
            };

            _context.Season.Add(season);
            _context.SaveChanges();

            //Act
            season.Path = "Updated Path";

            var seasonId = _seasonRepository.SaveSeason(season);

            //Assert
            Assert.Equal(season.SeasonId, seasonId);
        }

        [Fact]
        public void SaveSeason_GivenNonExistingSeason_ThenThrowsArgumentNullException()
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
            var exception = Record.Exception(() => _seasonRepository.SaveSeason(season));

            //Assert
            Assert.IsType<ArgumentNullException>(exception);
        }

        protected override void Dispose(bool disposing = true)
        {
            base.Dispose(disposing);

            _seasonRepository.Dispose();
        }
    }
}