using FileManager.Models;
using FileManager.Services;
using FileManager.Tests.Mocks;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xunit;

namespace FileManager.Tests.FileManagerServiceTests
{
    public class ShowServiceTests
    {
        [Fact]
        public async Task GetShowById_GivenValidId_ThenDoesNotThrow()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new Show())
            };

            var showService = new ShowService(new MockConfiguration(), mockHttpClientFactory, new MockLog());

            // Act
            var exception = await Record.ExceptionAsync(async () => await showService.GetAsync(1));

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public async Task GetShowById_GivenInvalidId_ThenThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new ArgumentOutOfRangeException())
            };

            var showService = new ShowService(new MockConfiguration(), mockHttpClientFactory, new MockLog());

            // Act
            var exception = await Record.ExceptionAsync(async () => await showService.GetAsync(0));

            // Assert
            Assert.IsType<ArgumentOutOfRangeException>(exception);
        }

        [Fact]
        public async Task GetShowByName_GivenValidName_ThenDoesNotThrow()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new Show())
            };

            var showService = new ShowService(new MockConfiguration(), mockHttpClientFactory, new MockLog());

            // Act
            var exception = await Record.ExceptionAsync(async () => await showService.GetAsync("Test"));

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public async Task GetShowByName_GivenInvalidName_ThenDoesNotThrow()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new ArgumentNullException())
            };

            var showService = new ShowService(new MockConfiguration(), mockHttpClientFactory, new MockLog());

            // Act
            var exception = await Record.ExceptionAsync(async () => await showService.GetAsync(string.Empty));

            // Assert
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task GetShows_ThenDoesNotThrow()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new List<Show>())
            };

            var showService = new ShowService(new MockConfiguration(), mockHttpClientFactory, new MockLog());

            // Act
            var exception = await Record.ExceptionAsync(async () => await showService.GetAsync());

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public async Task SaveShow_GivenVaildShow_ThenDoesNotThrow()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(1)
            };

            var showService = new ShowService(new MockConfiguration(), mockHttpClientFactory, new MockLog());

            // Act
            var exception = await Record.ExceptionAsync(async () => await showService.SaveAsync(new Show()));

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public async Task SaveShow_GivenInvaildShow_ThenThrowsArgumentNullException()
        {
            // Arrange
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new Show())
            };

            var showService = new ShowService(new MockConfiguration(), mockHttpClientFactory, new MockLog());

            // Act
            var exception = await Record.ExceptionAsync(async () => await showService.SaveAsync(null));

            // Assert
            Assert.IsType<ArgumentNullException>(exception);
        }
    }
}