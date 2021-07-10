using FileManager.Models;
using FileManager.Web.Controllers;
using FileManager.Web.Services.Interfaces;

using NSubstitute;

using System.Collections.Generic;
using System.Threading.Tasks;

using Xunit;

namespace FileManager.Tests.FileManagerWebTests
{
    public class ShowControllerTests
    {
        private readonly IControllerService<Show> _showControllerService;
        private readonly ShowController _showController;

        public ShowControllerTests()
        {
            _showControllerService = Substitute.For<IControllerService<Show>>();
            _showController = new ShowController(_showControllerService);
        }

        [Fact]
        public void Get_GivenNoParameter_ThenReturnsListOfShows()
        {
            // Arrange
            _showControllerService.Get()
                .Returns(new List<Show>());

            // Act
            var shows = _showController.Get().GetValue();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Show>>(shows);
        }

        [Fact]
        public async Task Get_GivenId_ThenShowIsReturned()
        {
            // Arrange
            var id = 1;

            _showControllerService.GetByIdAsync(Arg.Any<int>())
                .Returns(new Show { ShowId = id });

            // Act
            var show = (await _showController.GetById(id)).GetValue();

            // Assert
            Assert.Equal(id, show.ShowId);
        }

        [Fact]
        public void Get_GivenName_ThenShowIsReturned()
        {
            // Arrange
            var name = "Test Show";

            _showControllerService.GetByName(Arg.Any<string>())
                .Returns(new Show { Name = name });

            // Act
            var show = _showController.GetByName(name).GetValue();

            // Assert
            Assert.Equal(name, show.Name);
        }

        [Fact]
        public async Task Save_GivenValidShow_ThenReturnsShowId()
        {
            // Arrange
            var show = new Show
            {
                ShowId = 1,
                Name = "Test",
                Category = "Test",
                Path = "Test"
            };

            _showControllerService.SaveAsync(Arg.Any<Show>())
                .Returns(Task.CompletedTask);

            // Act
            show = (await _showController.Post(show)).GetValue();

            // Assert
            Assert.True(show.ShowId > 0);
        }
    }
}