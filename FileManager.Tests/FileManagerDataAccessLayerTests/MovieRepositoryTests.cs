using FileManager.DataAccessLayer;
using FileManager.DataAccessLayer.Repositories;
using FileManager.Models;
using FileManager.Models.Constants;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xunit;

namespace FileManager.Tests.FileManagerDataAccessLayerTests
{
    [Collection("Database collection")]
    public class MovieRepositoryTests
    {
        private readonly FileManagerContext _context;

        public MovieRepositoryTests(DatabaseFixture dbFixture)
        {
            _context = dbFixture.Context;
        }

        [Fact]
        public async Task GetMovieById_GivenValidId_ThenMovieIsReturned()
        {
            // Arrange
            var id = 1;

            var movieRepo = new MovieRepository(_context);

            // Act
            var movie = await movieRepo.GetByIdAsync(id);

            // Assert
            Assert.Equal(id, movie.MovieId);
        }

        [Fact]
        public void GetMovieByName_GivenValidName_ThenMovieIsReturned()
        {
            // Arrange
            var name = "Test";

            var movieRepo = new MovieRepository(_context);

            // Act
            var movie = movieRepo.GetByName(name);

            // Assert
            Assert.IsAssignableFrom<Movie>(movie);
            Assert.Equal(name, movie.Name);
        }

        [Fact]
        public void GetMovies_ThenReturnsMovieList()
        {
            // Arrange
            var movieRepo = new MovieRepository(_context);

            // Act
            var movies = movieRepo.Get();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Movie>>(movies);
        }

        [Fact]
        public async Task SaveMovie_GivenValidNewMovie_ThenReturnsMovieId()
        {
            // Arrange
            var movie = new Movie
            {
                MovieId = 0,
                SeriesId = 1,
                Category = "Test",
                IsSeries = false,
                Format = FileFormatTypes.MKV,
                Name = "Test Two",
                Path = "Some Path"
            };

            var movieRepo = new MovieRepository(_context);

            // Act
            await movieRepo.SaveAsync(movie);

            // Assert
            Assert.True(movie.MovieId > 0);
        }

        [Fact]
        public async Task SaveMovie_GivenValidExistingMovie_ThenMovieIdIsEqual()
        {
            // Arrange
            var movie = new Movie
            {
                MovieId = 1,
                SeriesId = 1,
                Category = "Test",
                IsSeries = false,
                Format = FileFormatTypes.MKV,
                Name = "Test",
                Path = "Some Path"
            };

            var movieRepo = new MovieRepository(_context);

            // Act
            movie.Path = "Updated Test Path";

            await movieRepo.SaveAsync(movie);

            // Assert
            Assert.Equal(1, movie.MovieId);
        }

        [Fact]
        public async Task SaveMovie_GivenNonExistingMovie_ThenThrowsArgumentNullException()
        {
            // Arrange
            var movie = new Movie
            {
                MovieId = 10,
                SeriesId = 1,
                Category = "Test",
                IsSeries = false,
                Format = FileFormatTypes.MKV,
                Name = "Test",
                Path = "Some Path"
            };

            var movieRepo = new MovieRepository(_context);

            // Act
            var exception = await Record.ExceptionAsync(async () => await movieRepo.SaveAsync(movie));

            // Assert
            Assert.IsType<ArgumentNullException>(exception);
        }
    }
}