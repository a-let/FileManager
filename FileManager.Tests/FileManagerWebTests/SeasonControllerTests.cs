using FileManager.Models;
using FileManager.Web.Controllers;
using FileManager.Web.Services.Interfaces;

using NSubstitute;

using System.Collections.Generic;
using System.Threading.Tasks;

using Xunit;

namespace FileManager.Tests.FileManagerWebTests
{
    public class SeasonControllerTests
    {
        private readonly IControllerService<Season> _seasonControllerService;
        private readonly SeasonController _seasonController;

        public SeasonControllerTests()
        {
            _seasonControllerService = Substitute.For<IControllerService<Season>>();
            _seasonController = new SeasonController(_seasonControllerService);
        }

        [Fact]
        public void Get_GivenNoParameter_ThenReturnsListOfSeasons()
        {
            // Arrange
            _seasonControllerService.Get()
                .Returns(new List<Season>());

            // Act
            var seasons = _seasonController.Get().GetValue();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Season>>(seasons);
        }

        [Fact]
        public async Task Get_GivenId_ThenSeasonIsReturned()
        {
            // Arrange
            var id = 1;

            _seasonControllerService.GetByIdAsync(Arg.Any<int>())
                .Returns(new Season { SeasonId = id });

            // Act
            var season = (await _seasonController.GetById(id)).GetValue();

            // Assert
            Assert.Equal(id, season.SeasonId);
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

            _seasonControllerService.SaveAsync(Arg.Any<Season>())
                .Returns(Task.CompletedTask);

            // Act
            season = (await _seasonController.Post(season)).GetValue();

            // Assert
            Assert.True(season.SeasonId > 0);
        }
    }
}