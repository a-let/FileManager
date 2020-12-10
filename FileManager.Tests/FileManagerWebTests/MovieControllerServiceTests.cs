using FileManager.Models;
using FileManager.Models.Constants;
using FileManager.Tests.Mocks;
using FileManager.Web.Services;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xunit;

namespace FileManager.Tests.FileManagerWebTests
{
    public class MovieControllerServiceTests
    {
        private readonly MovieControllerService _movieControllerService = new MovieControllerService(new MockMovieRepository());

        [Fact]
        public async Task GetMovieById_GivenInvalidId_ThenThrowsArgumentException()
        {
            // Arrange
            var id = 0;

            // Act
            var exception = await Record.ExceptionAsync(async () => await _movieControllerService.GetAsync(id));

            // Assert
            Assert.IsType<ArgumentException>(exception);
        }

        [Fact]
        public async Task GetMovieById_GivenValidId_ThenMovieIsReturned()
        {
            // Arrange
            var id = 1;

            // Act
            var movie = await _movieControllerService.GetAsync(id);

            // Assert
            Assert.IsAssignableFrom<Movie>(movie);
        }

        [Fact]
        public async Task GetMovies_ThenReturnsMovieList()
        {
            // Arrange, Act
            var movies = await _movieControllerService.GetAsync();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Movie>>(movies);
        }

        [Fact]
        public async Task GetMovieByName_GivenInvalidName_ThenThrowsArgumentNullException()
        {
            // Arrange
            var name = string.Empty;

            // Act
            var exception = await Record.ExceptionAsync(async () => await _movieControllerService.GetAsync(name));

            // Assert
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task GetMovieByName_GivenValidName_ThenMovieIsReturned()
        {
            // Arrange
            var name = "Test";

            // Act
            var movie = await _movieControllerService.GetAsync(name);

            // Assert
            Assert.IsAssignableFrom<Movie>(movie);
        }

        [Fact]
        public async Task SaveMovie_GivenNullMovie_ThenThrowsArgumentNullReferenceException()
        {
            // Arrange
            Movie movie = null;

            // Act
            var exception = await Record.ExceptionAsync(async () => await _movieControllerService.SaveAsync(movie));

            // Assert
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task SaveMovie_GivenNewMovie_ThenDoesNotThrow()
        {
            // Arrange
            var movie = new Movie
            {
                MovieId = 0,
                SeriesId = 1,
                Name = "Test",
                IsSeries = true,
                Format = FileFormatTypes.MP4,
                Category = "Test",
                Path = "Test"
            };

            // Act
            var exception = await Record.ExceptionAsync(async () => await _movieControllerService.SaveAsync(movie));

            // Assert
            Assert.Null(exception);
        }
    }
}