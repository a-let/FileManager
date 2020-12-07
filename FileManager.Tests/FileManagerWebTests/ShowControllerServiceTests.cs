using FileManager.Models;
using FileManager.Tests.Mocks;
using FileManager.Web.Services;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xunit;

namespace FileManager.Tests.FileManagerWebTests
{
    public class ShowControllerServiceTests
    {
        private readonly ShowControllerService _showControllerService = new ShowControllerService(new MockShowRepository());

        [Fact]
        public async Task GetShowById_GivenInvalidId_ThenThrowsArgumentException()
        {
            // Arrange
            var id = 0;

            // Act
            var exception = await Record.ExceptionAsync(async () => await _showControllerService.GetAsync(id));

            // Assert
            Assert.IsType<ArgumentException>(exception);
        }

        [Fact]
        public async Task GetShowById_GivenValidId_ThenShowIsReturned()
        {
            // Arrange
            var id = 1;

            // Act
            var show = await _showControllerService.GetAsync(id);

            // Assert
            Assert.IsAssignableFrom<Show>(show);
        }

        [Fact]
        public async Task GetShows_ThenReturnsShowList()
        {
            // Arrange, Act
            var shows = await _showControllerService.GetAsync();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Show>>(shows);
        }

        [Fact]
        public async Task GetShowByName_GivenInvalidName_ThenThrowsArgumentNullException()
        {
            // Arrange
            var name = string.Empty;

            // Act
            var exception = await Record.ExceptionAsync(async () => await _showControllerService.GetAsync(name));

            // Assert
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task GetShowByName_GivenValidName_ThenShowIsReturned()
        {
            // Arrange
            var name = "Test";

            // Act
            var show = await _showControllerService.GetAsync(name);

            // Assert
            Assert.IsAssignableFrom<Show>(show);
        }

        [Fact]
        public async Task SaveShow_GivenNullShow_ThenThrowsArgumentNullReferenceException()
        {
            // Arrange
            Show show = null;

            // Act
            var exception = await Record.ExceptionAsync(async () => await _showControllerService.SaveAsync(show));

            // Assert
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task SaveShow_GivenShow_ThenReturnsShowId()
        {
            // Arrange
            var show = new Show();

            // Act
            var showId = await _showControllerService.SaveAsync(show);

            // Assert
            Assert.True(showId > 0);
        }
    }
}