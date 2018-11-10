using FileManager.DataAccessLayer.Repositories;
using FileManager.Models;
using FileManager.Models.Constants;
using System;
using System.Collections.Generic;
using System.Linq;

using Xunit;

namespace FileManager.Tests.FileManagerDataAccessLayerTests
{
    public class MovieRepositoryTests : TestBase
    {
        private readonly MovieRepository _movieRepository;

        public MovieRepositoryTests() : base(nameof(MovieRepositoryTests))
        {
            _movieRepository = new MovieRepository(_context);
        }

        [Fact]
        public void GetMovieById_GivenValidId_ThenMovieIsReturned()
        {
            //Arrange
            var id = 1;

            _context.Movie.Add(new Movie
            {
                MovieId = 1,
                SeriesId = 1,
                Category = "Test",
                IsSeries = false,
                Format = FileFormatTypes.MKV,
                Name = "Test",
                Path = "Some Path"
            });

            _context.SaveChanges();

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

            _context.Movie.Add(new Movie
            {
                MovieId = 0,
                SeriesId = 1,
                Category = "Test",
                IsSeries = false,
                Format = FileFormatTypes.MKV,
                Name = name,
                Path = "Some Path"
            });

            _context.SaveChanges();

            //Act
            var movie = _movieRepository.GetMovieByName(name);

            //Assert
            Assert.IsAssignableFrom<Movie>(movie);
            Assert.Equal(name, movie.Name);
        }

        [Fact]
        public void GetMovies_ThenReturnsEpisodeList()
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

            _context.Movie.Add(new Movie
            {
                MovieId = 0,
                SeriesId = id,
                Category = "Test",
                IsSeries = false,
                Format = FileFormatTypes.MKV,
                Name = "Test",
                Path = "Some Path"
            });

            _context.SaveChanges();

            //Act
            var movies = _movieRepository.GetMoviesBySeriesId(id);

            //Assert
            Assert.IsAssignableFrom<IEnumerable<Movie>>(movies);
            Assert.True(movies.Count() == 1);
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
                Name = "Test",
                Path = "Some Path"
            };

            //Act
            var movieId = _movieRepository.SaveMovie(movie);

            //Assert
            Assert.True(movieId > 0);
        }

        [Fact]
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

            _context.Movie.Add(movie);
            _context.SaveChanges();

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

        protected override void Dispose(bool disposing = true)
        {
            base.Dispose(disposing);

            _movieRepository.Dispose();
        }
    }
}