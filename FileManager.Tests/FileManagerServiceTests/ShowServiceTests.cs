using System;

using Xunit;

using FileManager.Models;
using FileManager.Services;
using FileManager.Tests.Mocks;

namespace FileManager.Tests.FileManagerServiceTests
{
    public class ShowServiceTests
    {
        //private readonly ShowService _showService = new ShowService(new MockConfiguration(), new MockHttpClientFactory());
        private readonly ShowService _showService = new ShowService(new MockConfiguration(), null);

        [Fact]
        public void GetShowById_GivenValidId_ThenDoesNotThrow()
        {
            //Arrange
            var id = 1;

            //Act
            var exception = Record.Exception(() => _showService.GetShowById(id));

            //Assert
            Assert.Null(exception);
        }

        [Fact]
        public void GetShowById_GivenInvalidId_ThenThrowsArgumentOutOfRangeException()
        {
            //Arrange
            var id = 0;

            //Act
            var exception = Record.Exception(() => _showService.GetShowById(id));

            //Assert
            Assert.IsType<ArgumentOutOfRangeException>(exception.InnerException);
        }

        [Fact]
        public void GetShowByName_GivenValidName_ThenDoesNotThrow()
        {
            //Arrange
            var name = "Test Show Name";

            //Act
            var exception = Record.Exception(() => _showService.GetShowByName(name));

            //Assert
            Assert.Null(exception);
        }

        [Fact]
        public void GetShowByName_GivenInvalidName_ThenDoesNotThrow()
        {
            //Arrange
            var name = "";

            //Act
            var exception = Record.Exception(() => _showService.GetShowByName(name));

            //Assert
            Assert.IsType<ArgumentNullException>(exception.InnerException);
        }

        [Fact]
        public void GetShows_ThenDoesNotThrow()
        {
            //Arrange, Act
            var exception = Record.Exception(() => _showService.GetShows());

            //Assert
            Assert.Null(exception);
        }

        [Fact]
        public void SaveShow_GivenVaildShow_ThenDoesNotThrow()
        {
            //Arrange
            var show = new Show();

            //Act
            var exception = Record.Exception(() => _showService.SaveShow(show));

            //Assert
            Assert.Null(exception);
        }

        [Fact]
        public void SaveShow_GivenInvaildShow_ThenThrowsArgumentNullException()
        {
            //Arrange, Act
            var exception = Record.Exception(() => _showService.SaveShow(null));

            //Assert
            Assert.IsType<ArgumentNullException>(exception.InnerException);
        }
    }
}