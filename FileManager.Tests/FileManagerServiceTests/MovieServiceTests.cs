using FileManager.Models;
using FileManager.Services;
using FileManager.Tests.Mocks;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xunit;

namespace FileManager.Tests.FileManagerServiceTests
{
    public class MovieServiceTests
    {
        [Fact]
        public async Task GetMovies_ThenDoesNotThrow()
        {
            // Act
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new List<Movie>())
            };

            var movieService = new MovieService(new MockConfiguration(), mockHttpClientFactory, new MockLog());

            // Arrange
            var exception = await Record.ExceptionAsync(async () => await movieService.GetAsync());

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public async Task GetMovieById_GivenValidId_ThenDoesNotThrow()
        {
            // Act
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new Movie())
            };

            var movieService = new MovieService(new MockConfiguration(), mockHttpClientFactory, new MockLog());

            // Arrange
            var exception = await Record.ExceptionAsync(async () => await movieService.GetAsync(1));

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public async Task GetMovieById_GivenIdLessThanOne_ThenThrowsArgumentOutOfRangeException()
        {
            // Act
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new ArgumentOutOfRangeException())
            };

            var movieService = new MovieService(new MockConfiguration(), mockHttpClientFactory, new MockLog());

            // Arrange
            var exception = await Record.ExceptionAsync(async () => await movieService.GetAsync(0));

            // Assert
            Assert.IsType<ArgumentOutOfRangeException>(exception);
        }

        [Fact]
        public async Task GetMovieByName_GivenValidName_ThenDoesNotThrow()
        {
            // Act
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new Movie())
            };

            var movieService = new MovieService(new MockConfiguration(), mockHttpClientFactory, new MockLog());

            // Arrange
            var exception = await Record.ExceptionAsync(async () => await movieService.GetAsync("Test Name"));

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public async Task GetMovieByName_GivenInvalidName_ThenThrowsArgumentNullException()
        {
            // Act
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new ArgumentNullException())
            };

            var movieService = new MovieService(new MockConfiguration(), mockHttpClientFactory, new MockLog());

            // Arrange
            var exception = await Record.ExceptionAsync(async () => await movieService.GetAsync(""));

            // Assert
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task SaveMovie_GivenValidMovie_ThenDoesNotThrow()
        {
            // Act
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(1)
            };

            var movieService = new MovieService(new MockConfiguration(), mockHttpClientFactory, new MockLog());

            // Arrange
            var movie = new Movie();

            var exception = await Record.ExceptionAsync(async () => await movieService.SaveAsync(movie));

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public async Task SaveMovie_GivenNullMovie_ThenThrowsArgumentNullException()
        {
            // Act
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new ArgumentNullException())
            };

            var movieService = new MovieService(new MockConfiguration(), mockHttpClientFactory, new MockLog());

            // Arrange
            var exception = await Record.ExceptionAsync(async () => await movieService.SaveAsync(null));

            // Assert
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task GetMoviesBySeasonId_GivenValidSeasonId_ThenDoesNotThrow()
        {
            // Act
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new List<Movie>())
            };

            var movieService = new MovieService(new MockConfiguration(), mockHttpClientFactory, new MockLog());

            // Arrange
            var exception = await Record.ExceptionAsync(async () => await movieService.GetMoviesBySeriesId(1));

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public async Task GetMoviesBySeriesId_GivenSeriesIdLessThanOne_ThenThrowsArgumentOutOfRangeException()
        {
            // Act
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new ArgumentOutOfRangeException())
            };

            var movieService = new MovieService(new MockConfiguration(), mockHttpClientFactory, new MockLog());

            // Arrange
            var exception = await Record.ExceptionAsync(async () => await movieService.GetMoviesBySeriesId(0));

            // Assert
            Assert.IsType<ArgumentOutOfRangeException>(exception);
        }
    }
}