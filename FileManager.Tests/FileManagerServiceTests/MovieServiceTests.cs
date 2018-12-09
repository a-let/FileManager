using FileManager.Models;
using FileManager.Services;
using FileManager.Tests.Mocks;

using System;
using System.Collections.Generic;

using Xunit;

namespace FileManager.Tests.FileManagerServiceTests
{
    public class MovieServiceTests
    {
        [Fact]
        public void GetMovies_ThenDoesNotThrow()
        {
            // Act
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new List<Movie>())
            };

            var movieService = new MovieService(new MockConfiguration(), mockHttpClientFactory);

            // Arrange
            var exception = Record.ExceptionAsync(() => movieService.GetMovies());

            // Assert
            Assert.Null(exception.Result);
        }

        [Fact]
        public void GetMovieById_GivenValidId_ThenDoesNotThrow()
        {
            // Act
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new Movie())
            };

            var movieService = new MovieService(new MockConfiguration(), mockHttpClientFactory);

            // Arrange
            var exception = Record.ExceptionAsync(() => movieService.GetMovieById(1));

            // Assert
            Assert.Null(exception.Result);
        }

        [Fact]
        public void GetMovieById_GivenIdLessThanOne_ThenThrowsArgumentOutOfRangeException()
        {
            // Act
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new ArgumentOutOfRangeException())
            };

            var movieService = new MovieService(new MockConfiguration(), mockHttpClientFactory);

            // Arrange
            var exception = Record.ExceptionAsync(() => movieService.GetMovieById(0));

            // Assert
            Assert.IsType<ArgumentOutOfRangeException>(exception.Result.InnerException);
        }

        [Fact]
        public void GetMovieByName_GivenValidName_ThenDoesNotThrow()
        {
            // Act
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new Movie())
            };

            var movieService = new MovieService(new MockConfiguration(), mockHttpClientFactory);

            // Arrange
            var exception = Record.ExceptionAsync(() => movieService.GetMovieByName("Test Name"));

            // Assert
            Assert.Null(exception.Result);
        }

        [Fact]
        public void GetMovieByName_GivenInvalidName_ThenThrowsArgumentNullException()
        {
            // Act
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new ArgumentNullException())
            };

            var movieService = new MovieService(new MockConfiguration(), mockHttpClientFactory);

            // Arrange
            var exception = Record.ExceptionAsync(() => movieService.GetMovieByName(""));

            // Assert
            Assert.IsType<ArgumentNullException>(exception.Result.InnerException);
        }

        [Fact]
        public void SaveMovie_GivenValidMovie_ThenDoesNotThrow()
        {
            // Act
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(1)
            };

            var movieService = new MovieService(new MockConfiguration(), mockHttpClientFactory);

            // Arrange
            var movie = new Movie();

            var exception = Record.ExceptionAsync(() => movieService.SaveMovie(movie));

            // Assert
            Assert.Null(exception.Result);
        }

        [Fact]
        public void SaveMovie_GivenNullMovie_ThenThrowsArgumentNullException()
        {
            // Act
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new ArgumentNullException())
            };

            var movieService = new MovieService(new MockConfiguration(), mockHttpClientFactory);

            // Arrange
            var exception = Record.ExceptionAsync(() => movieService.SaveMovie(null));

            // Assert
            Assert.IsType<ArgumentNullException>(exception.Result.InnerException);
        }

        [Fact]
        public void GetMoviesBySeasonId_GivenValidSeasonId_ThenDoesNotThrow()
        {
            // Act
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new List<Movie>())
            };

            var movieService = new MovieService(new MockConfiguration(), mockHttpClientFactory);

            // Arrange
            var exception = Record.ExceptionAsync(() => movieService.GetMoviesBySeriesId(1));

            // Assert
            Assert.Null(exception.Result);
        }

        [Fact]
        public void GetMoviesBySeriesId_GivenSeriesIdLessThanOne_ThenThrowsArgumentOutOfRangeException()
        {
            // Act
            var mockHttpClientFactory = new MockHttpClientFactory
            {
                FakeHttpMessageHandler = new FakeHttpMessageHandler(new ArgumentOutOfRangeException())
            };

            var movieService = new MovieService(new MockConfiguration(), mockHttpClientFactory);

            // Arrange
            var exception = Record.ExceptionAsync(() => movieService.GetMoviesBySeriesId(0));

            // Assert
            Assert.IsType<ArgumentOutOfRangeException>(exception.Result.InnerException);
        }
    }
}