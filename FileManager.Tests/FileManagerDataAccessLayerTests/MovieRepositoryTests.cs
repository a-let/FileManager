using FileManager.DataAccessLayer.Repositories;
using FileManager.Models;
using FileManager.Models.Constants;
using System;
using System.Collections.Generic;
using Xunit;

namespace FileManager.Tests.FileManagerDataAccessLayerTests
{
    [Collection("Database collection")]
    public class MovieRepositoryTests
    {
        private readonly MovieRepository _movieRepository;

        public MovieRepositoryTests(DatabaseFixture dbFixture)
        {
            _movieRepository = dbFixture.MovieRepo;
        }

        [Fact]
        public void GetMovieById_GivenValidId_ThenMovieIsReturned()
        {
            //Arrange
            var id = 1;

            //Act
            var movie = _movieRepository.GetMovieById(id);

            //Assert
            Assert.Equal(id, movie.MovieId);
        }

        [Fact]
        public void GetMovieByName_GivenValidName_ThenMovieIsReturned()
        {
            //Arrange
            var name = "Test";

            //Act
            var movie = _movieRepository.GetMovieByName(name);

            //Assert
            Assert.IsAssignableFrom<Movie>(movie);
            Assert.Equal(name, movie.Name);
        }

        [Fact]
        public void GetMovies_ThenReturnsMovieList()
        {
            //Arrange, Act
            var movies = _movieRepository.GetMovies();

            //Assert
            Assert.IsAssignableFrom<IEnumerable<Movie>>(movies);
        }

        [Fact]
        public void GetMoviesBySeriesId_GivenValidSeriesId_ThenMoviesReturned()
        {
            //Arrange
            var id = 1;

            //Act
            var movies = _movieRepository.GetMoviesBySeriesId(id);

            //Assert
            Assert.IsAssignableFrom<IEnumerable<Movie>>(movies);
            Assert.NotEmpty(movies);
        }

        [Fact]
        public void SaveMovie_GivenValidNewMovie_ThenReturnsMovieId()
        {
            //Arrange
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

            //Act
            var movieId = _movieRepository.SaveMovie(movie);

            //Assert
            Assert.True(movieId > 0);
        }

        [Fact(Skip = "Test is just saving a new record. Fix after DAL refactor.")]
        public void SaveMovie_GivenValidExistingMovie_ThenMovieIdIsEqual()
        {
            //Arrange
            var movie = new Movie
            {
                MovieId = 0,
                SeriesId = 1,
                Category = "Test",
                IsSeries = false,
                Format = FileFormatTypes.MKV,
                Name = "Test",
                Path = "Some Path"
            };

            //Act
            movie.Name = "Updated Name";

            var movieId = _movieRepository.SaveMovie(movie);

            //Assert
            Assert.Equal(movie.MovieId, movieId);
        }

        [Fact]
        public void SaveMovie_GivenNonExistingMovie_ThenThrowsArgumentNullException()
        {
            //Arrange
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

            //Act
            var exception = Record.Exception(() => _movieRepository.SaveMovie(movie));

            //Assert
            Assert.IsType<ArgumentNullException>(exception);
        }
    }
}