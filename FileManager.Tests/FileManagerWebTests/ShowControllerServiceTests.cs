using System;
using System.Collections.Generic;

using Xunit;

using FileManager.Models;
using FileManager.Tests.Mocks;
using FileManager.Web.Services;

namespace FileManager.Tests.FileManagerWebTests
{
    public class ShowControllerServiceTests
    {
        private readonly ShowControllerService _showControllerService = new ShowControllerService(new MockShowAdapter());

        [Fact]
        public void GetShowById_GivenInvalidId_ThenThrowsArgumentException()
        {
            //Arrange
            var id = 0;

            //Act
            var exception = Record.Exception(() => _showControllerService.GetShowById(id));

            //Assert
            Assert.IsType<ArgumentException>(exception);
        }

        [Fact]
        public void GetShowById_GivenValidId_ThenShowIsReturned()
        {
            //Arrange
            var id = 1;

            //Act
            var show = _showControllerService.GetShowById(id);

            //Assert
            Assert.IsAssignableFrom<Show>(show);
        }

        [Fact]
        public void GetShows_ThenReturnsShowList()
        {
            //Arrange, Act
            var shows = _showControllerService.GetShows();

            //Assert
            Assert.IsAssignableFrom<IEnumerable<Show>>(shows);
        }

        [Fact]
        public void GetShowByName_GivenInvalidName_ThenThrowsArgumentNullException()
        {
            //Arrange
            var name = string.Empty;

            //Act
            var exception = Record.Exception(() => _showControllerService.GetShowByName(name));

            //Assert
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void GetShowByName_GivenValidName_ThenShowIsReturned()
        {
            //Arrange
            var name = "Test";

            //Act
            var show = _showControllerService.GetShowByName(name);

            //Assert
            Assert.IsAssignableFrom<Show>(show);
        }

        [Fact]
        public void SaveShow_GivenNullShow_ThenThrowsArgumentNullReferenceException()
        {
            //Arrange
            Show show = null;

            //Act
            var exception = Record.Exception(() => _showControllerService.SaveShow(show));

            //Assert
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void SaveShow_GivenShow_ThenReturnsTrue()
        {
            //Arrange
            var show = new Show();

            //Act
            var success = _showControllerService.SaveShow(show);

            //Assert
            Assert.True(success);
        }
    }
}