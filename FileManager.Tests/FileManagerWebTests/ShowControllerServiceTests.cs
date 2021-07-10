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
    public class ShowControllerServiceTests
    {
        private readonly IRepository<Show> _showRepo;
        private readonly ShowControllerService _showControllerService;

        public ShowControllerServiceTests()
        {
            _showRepo = Substitute.For<IRepository<Show>>();
            _showControllerService = new ShowControllerService(_showRepo);
        }

        [Fact]
        public async Task GetShowById_GivenInvalidId_ThenThrowsArgumentException()
        {
            // Arrange
            var id = 0;

            // Act
            var exception = await Record.ExceptionAsync(async () => await _showControllerService.GetByIdAsync(id));

            // Assert
            Assert.IsType<ArgumentException>(exception);
        }

        [Fact]
        public async Task GetShowById_GivenValidId_ThenShowIsReturned()
        {
            // Arrange
            var id = 1;

            _showRepo.GetByIdAsync(Arg.Any<int>())
                .Returns(new Show());

            // Act
            var show = await _showControllerService.GetByIdAsync(id);

            // Assert
            Assert.IsAssignableFrom<Show>(show);
        }

        [Fact]
        public void GetShows_ThenReturnsShowList()
        {
            // Arrange
            _showRepo.Get()
                .Returns(new List<Show>());

            // Act
            var shows = _showControllerService.Get();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Show>>(shows);
        }

        [Fact]
        public void GetShowByName_GivenInvalidName_ThenThrowsArgumentNullException()
        {
            // Arrange
            var name = string.Empty;

            // Act
            var exception = Record.Exception(() => _showControllerService.GetByName(name));

            // Assert
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void GetShowByName_GivenValidName_ThenShowIsReturned()
        {
            // Arrange
            var name = "Test";

            _showRepo.GetByName(Arg.Any<string>())
                .Returns(new Show());

            // Act
            var show = _showControllerService.GetByName(name);

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

            _showRepo.SaveAsync(Arg.Any<Show>())
                .Returns(Task.CompletedTask);

            // Act
            var exception = await Record.ExceptionAsync(async () => await _showControllerService.SaveAsync(show));

            // Assert
            Assert.Null(exception);
        }
    }
}