using FileManager.DataAccessLayer.Interfaces;
using FileManager.Models;
using FileManager.Web.Services;

using NSubstitute;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xunit;

namespace FileManager.Tests.FileManagerWebTests
{
    public class SeasonControllerServiceTests
    {
        private readonly IRepository<Season> _seasonRepo;
        private readonly SeasonControllerService _seasonControllerService;

        public SeasonControllerServiceTests()
        {
            _seasonRepo = Substitute.For<IRepository<Season>>();
            _seasonControllerService = new SeasonControllerService(_seasonRepo);
        }

        [Fact]
        public async Task GetSeasonById_GivenInvalidId_ThenThrowsArgumentException()
        {
            // Arrange
            var id = 0;

            // Act
            var exception = await Record.ExceptionAsync(async () => await _seasonControllerService.GetByIdAsync(id));

            // Assert
            Assert.IsType<ArgumentException>(exception);
        }

        [Fact]
        public async Task GetSeasonById_GivenValidId_ThenSeasonIsReturned()
        {
            // Arrange
            var id = 1;

            _seasonRepo.GetByIdAsync(Arg.Any<int>())
                .Returns(new Season());

            // Act
            var season = await _seasonControllerService.GetByIdAsync(id);

            // Assert
            Assert.IsAssignableFrom<Season>(season);
        }

        [Fact]
        public void GetSeasons_ThenReturnsSeasonList()
        {
            // Arrange
            _seasonRepo.Get()
                .Returns(new List<Season>());

            // Act
            var seasons = _seasonControllerService.Get();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Season>>(seasons);
        }

        [Fact]
        public async Task SaveSeason_GivenNullSeason_ThenThrowsArgumentNullReferenceException()
        {
            // Arrange
            Season season = null;

            // Act
            var exception = await Record.ExceptionAsync(async () =>  await _seasonControllerService.SaveAsync(season));

            // Assert
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task SaveSeason_GivenSeason_ThenReturnsSeasonId()
        {
            // Arrange
            var season = new Season
            {

            };

            _seasonRepo.SaveAsync(Arg.Any<Season>())
                .Returns(Task.CompletedTask);

            // Act
            var exception = await Record.ExceptionAsync(async () => await _seasonControllerService.SaveAsync(season));

            // Assert
            Assert.Null(exception);
        }
    }
}