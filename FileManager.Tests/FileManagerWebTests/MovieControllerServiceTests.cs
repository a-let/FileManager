using FileManager.DataAccessLayer.Interfaces;
using FileManager.Models;
using FileManager.Models.Constants;
using FileManager.Web.Services;

using NSubstitute;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xunit;

namespace FileManager.Tests.FileManagerWebTests
{
    public class MovieControllerServiceTests
    {
        private readonly IRepository<Movie> _movieRepo;
        private readonly MovieControllerService _movieControllerService;

        public MovieControllerServiceTests()
        {
            _movieRepo = Substitute.For<IRepository<Movie>>();
            _movieControllerService = new MovieControllerService(_movieRepo);
        }

        [Fact]
        public async Task GetMovieById_GivenInvalidId_ThenThrowsArgumentException()
        {
            // Arrange
            var id = 0;

            // Act
            var exception = await Record.ExceptionAsync(async () => await _movieControllerService.GetByIdAsync(id));

            // Assert
            Assert.IsType<ArgumentException>(exception);
        }

        [Fact]
        public async Task GetMovieById_GivenValidId_ThenMovieIsReturned()
        {
            // Arrange
            var id = 1;

            _movieRepo.GetByIdAsync(Arg.Any<int>())
                .Returns(new Movie());

            // Act
            var movie = await _movieControllerService.GetByIdAsync(id);

            // Assert
            Assert.IsAssignableFrom<Movie>(movie);
        }

        [Fact]
        public void GetMovies_ThenReturnsMovieList()
        {
            // Arrange
            _movieRepo.Get()
                .Returns(new List<Movie>());

            // Act
            var movies = _movieControllerService.Get();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Movie>>(movies);
        }

        [Fact]
        public void GetMovieByName_GivenInvalidName_ThenThrowsArgumentNullException()
        {
            // Arrange
            var name = string.Empty;

            // Act
            var exception = Record.Exception(() => _movieControllerService.GetByName(name));

            // Assert
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void GetMovieByName_GivenValidName_ThenMovieIsReturned()
        {
            // Arrange
            var name = "Test";

            _movieRepo.GetByName(Arg.Any<string>())
                .Returns(new Movie());

            // Act
            var movie = _movieControllerService.GetByName(name);

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

            _movieRepo.SaveAsync(Arg.Any<Movie>())
                .Returns(Task.CompletedTask);

            // Act
            var exception = await Record.ExceptionAsync(async () => await _movieControllerService.SaveAsync(movie));

            // Assert
            Assert.Null(exception);
        }
    }
}