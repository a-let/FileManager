using FileManager.DataAccessLayer.Repositories;
using FileManager.Models;

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

            _context.Seasons.Add(new Season
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

            _context.Seasons.Add(new Season
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
        public void SaveSeason_GivenValidNewSeason_ThenReturnsTrue()
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
            var success = _seasonRepository.SaveSeason(season);

            //Assert
            Assert.True(success);
        }

        [Fact]
        public void SaveSeason_GivenValidExistingSeason_ThenReturnsTrue()
        {
            //Arrange
            var season = new Season
            {
                SeasonId = 1,
                ShowId = 1,
                SeasonNumber = 1,
                Path = "Test"
            };

            _context.Seasons.Add(season);
            _context.SaveChanges();

            //Act
            season.SeasonId = 1;
            season.Path = "Updated Path";

            var success = _seasonRepository.SaveSeason(season);

            //Assert
            Assert.True(success);
        }

        [Fact]
        public void SaveSeason_GivenNonExistingSeason_ThenReturnsFalse()
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
            var success = _seasonRepository.SaveSeason(season);

            //Assert
            Assert.False(success);
        }

        protected override void Dispose(bool disposing = true)
        {
            base.Dispose(disposing);

            _seasonRepository.Dispose();
        }
    }
}