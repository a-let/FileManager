using FileManager.DataAccessLayer;
using FileManager.Models;
using FileManager.Models.Constants;
using FileManager.Web.Services;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;

using Xunit;

namespace FileManager.Tests.FileManagerWebTests
{
    public class MovieControllerServiceTests : IDisposable
    {
        private readonly FileManagerContext _context;
        private readonly MovieControllerService _movieControllerService;

        public MovieControllerServiceTests()
        {
            var options = new DbContextOptionsBuilder<FileManagerContext>()
                .UseInMemoryDatabase(databaseName: "MovieControllerServiceTests")
                .Options;

            _context = new FileManagerContext(options);

            _movieControllerService = new MovieControllerService(_context);
        }

        [Fact]
        public void GetMovieById_GivenInvalidId_ThenThrowsArgumentException()
        {
            //Arrange
            var id = 0;

            //Act
            var exception = Record.Exception(() => _movieControllerService.GetMovieById(id));

            //Assert
            Assert.IsType<ArgumentException>(exception);
        }

        [Fact]
        public void GetMovieById_GivenValidId_ThenMovieIsReturned()
        {
            //Arrange
            var id = 1;

            _context.Movies.Add(new Movie
            {
                MovieId = id,
                SeriesId = 1,
                Name = "Test",
                IsSeries = true,
                Format = FileFormatTypes.MP4,
                Category = "Test",
                Path = "Test"
            });

            _context.SaveChanges();

            //Act
            var movie = _movieControllerService.GetMovieById(id);

            //Assert
            Assert.IsAssignableFrom<Movie>(movie);
        }

        [Fact]
        public void GetMovies_ThenReturnsMovieList()
        {
            //Arrange, Act
            var movies = _movieControllerService.GetMovies();

            //Assert
            Assert.IsAssignableFrom<IEnumerable<Movie>>(movies);
        }

        [Fact]
        public void GetMovieByName_GivenInvalidName_ThenThrowsArgumentNullException()
        {
            //Arrange
            var name = string.Empty;

            //Act
            var exception = Record.Exception(() => _movieControllerService.GetMovieByName(name));

            //Assert
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void GetMovieByName_GivenValidName_ThenMovieIsReturned()
        {
            //Arrange
            var name = "Test";

            _context.Movies.Add(new Movie
            {
                MovieId = 1,
                SeriesId = 1,
                Name = name,
                IsSeries = true,
                Format = FileFormatTypes.MP4,
                Category = "Test",
                Path = "Test"
            });

            _context.SaveChanges();

            //Act
            var movie = _movieControllerService.GetMovieByName(name);

            //Assert
            Assert.IsAssignableFrom<Movie>(movie);
            Assert.Equal(name, movie.Name);
        }

        [Fact]
        public void SaveMovie_GivenNullMovie_ThenThrowsArgumentNullReferenceException()
        {
            //Arrange
            Movie movie = null;

            //Act
            var exception = Record.Exception(() => _movieControllerService.SaveMovie(movie));

            //Assert
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void SaveMovie_GivenNewMovie_ThenDoesNotThrow()
        {
            //Arrange
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

            //Act
            var exception = Record.Exception(() => _movieControllerService.SaveMovie(movie));

            //Assert
            Assert.Null(exception);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}