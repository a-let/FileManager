using FileManager.Models;
using FileManager.Tests.Mocks;
using FileManager.Web.Controllers;

using System.Collections.Generic;
using System.Threading.Tasks;

using Xunit;

namespace FileManager.Tests.FileManagerWebTests
{
    public class ShowControllerTests
    {
        private readonly ShowController _showController = new ShowController(new MockShowControllerService());

        [Fact]
        public async Task Get_GivenNoParameter_ThenReturnsListOfShows()
        {
            // Arrange

            // Act
            var shows = (await _showController.Get()).GetValue();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Show>>(shows);
        }

        [Fact]
        public async Task Get_GivenId_ThenShowIsReturned()
        {
            // Arrange
            var id = 1;

            // Act
            var show = (await _showController.GetById(id)).GetValue();

            // Assert
            Assert.Equal(id, show.ShowId);
        }

        [Fact]
        public async Task Get_GivenName_ThenShowIsReturned()
        {
            // Arrange
            var name = "Test Show";

            // Act
            var show = (await _showController.GetByName(name)).GetValue();

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

            // Act
            show = (await _showController.Post(show)).GetValue();

            // Assert
            Assert.True(show.ShowId > 0);
        }
    }
}