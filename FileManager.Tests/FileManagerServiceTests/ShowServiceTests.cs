using FileManager.Models;
using FileManager.Services;
using FileManager.Tests.Mocks;

using System;
using System.Collections.Generic;

using Xunit;

namespace FileManager.Tests.FileManagerServiceTests
{
    public class ShowServiceTests
    {
        [Fact]
        public void GetShowById_GivenValidId_ThenDoesNotThrow()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new Show())
            };

            var showService = new ShowService(new MockConfiguration(), mockHttpClientFactory);

            // Act
            var exception = Record.ExceptionAsync(() => showService.GetShowById(1));

            // Assert
            Assert.Null(exception.Result);
        }

        [Fact]
        public void GetShowById_GivenInvalidId_ThenThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new ArgumentOutOfRangeException())
            };

            var showService = new ShowService(new MockConfiguration(), mockHttpClientFactory);

            // Act
            var exception = Record.ExceptionAsync(() => showService.GetShowById(0));

            // Assert
            Assert.IsType<ArgumentOutOfRangeException>(exception.Result.InnerException);
        }

        [Fact]
        public void GetShowByName_GivenValidName_ThenDoesNotThrow()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new Show())
            };

            var showService = new ShowService(new MockConfiguration(), mockHttpClientFactory);

            // Act
            var exception = Record.ExceptionAsync(() => showService.GetShowByName("Test"));

            // Assert
            Assert.Null(exception.Result);
        }

        [Fact]
        public void GetShowByName_GivenInvalidName_ThenDoesNotThrow()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new ArgumentNullException())
            };

            var showService = new ShowService(new MockConfiguration(), mockHttpClientFactory);

            // Act
            var exception = Record.ExceptionAsync(() => showService.GetShowByName(string.Empty));

            // Assert
            Assert.IsType<ArgumentNullException>(exception.Result.InnerException);
        }

        [Fact]
        public void GetShows_ThenDoesNotThrow()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new List<Show>())
            };

            var showService = new ShowService(new MockConfiguration(), mockHttpClientFactory);

            // Act
            var exception = Record.ExceptionAsync(() => showService.GetShows());

            // Assert
            Assert.Null(exception.Result);
        }

        [Fact]
        public void SaveShow_GivenVaildShow_ThenDoesNotThrow()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(1)
            };

            var showService = new ShowService(new MockConfiguration(), mockHttpClientFactory);

            // Act
            var exception = Record.ExceptionAsync(() => showService.SaveShow(new Show()));

            // Assert
            Assert.Null(exception.Result);
        }

        [Fact]
        public void SaveShow_GivenInvaildShow_ThenThrowsArgumentNullException()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new Show())
            };

            var showService = new ShowService(new MockConfiguration(), mockHttpClientFactory);

            // Act
            var exception = Record.ExceptionAsync(() => showService.SaveShow(null));

            // Assert
            Assert.IsType<ArgumentNullException>(exception.Result.InnerException);
        }
    }
}