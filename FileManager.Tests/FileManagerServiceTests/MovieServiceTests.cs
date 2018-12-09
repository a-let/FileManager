using System;

using Xunit;

using FileManager.Models;
using FileManager.Services;
using FileManager.Tests.Mocks;

namespace FileManager.Tests.FileManagerServiceTests
{
    public class MovieServiceTests
    {
        //private readonly MovieService _movieService = new MovieService(new MockConfiguration(), new MockHttpClientFactory());
        private readonly MovieService _movieService = new MovieService(new MockConfiguration(), null);

        [Fact]
        public void GetMovies_ThenDoesNotThrow()
        {
            //Arrange, Act
            var exception = Record.Exception(() => _movieService.GetMovies());

            //Assert
            Assert.Null(exception);
        }

        [Fact]
        public void GetMovieById_GivenValidId_ThenDoesNotThrow()
        {
            //Arrange, Act
            var exception = Record.Exception(() => _movieService.GetMovieById(1));

            //Assert
            Assert.Null(exception);
        }

        [Fact]
        public void GetMovieById_GivenIdLessThanOne_ThenThrowsArgumentOutOfRangeException()
        {
            //Arrange, Act
            var exception = Record.Exception(() => _movieService.GetMovieById(0));

            //Assert
            Assert.IsType<ArgumentOutOfRangeException>(exception.InnerException);
        }

        [Fact]
        public void GetMovieByName_GivenValidName_ThenDoesNotThrow()
        {
            //Arrange, Act
            var exception = Record.Exception(() => _movieService.GetMovieByName("Test Name"));

            //Assert
            Assert.Null(exception);
        }

        [Fact]
        public void GetMovieByName_GivenInvalidName_ThenThrowsArgumentNullException()
        {
            //Arrange, Act
            var exception = Record.Exception(() => _movieService.GetMovieByName(""));

            //Assert
            Assert.IsType<ArgumentNullException>(exception.InnerException);
        }

        [Fact]
        public void SaveMovie_GivenValidMovie_ThenDoesNotThrow()
        {
            //Arrange, Act
            var movie = new Movie();

            var exception = Record.Exception(() => _movieService.SaveMovie(movie));

            //Assert
            Assert.Null(exception);
        }

        [Fact]
        public void SaveMovie_GivenNullMovie_ThenThrowsArgumentNullException()
        {
            //Arrange, Act
            var exception = Record.Exception(() => _movieService.SaveMovie(null));

            //Assert
            Assert.IsType<ArgumentNullException>(exception.InnerException);
        }

        [Fact]
        public void GetMoviesBySeasonId_GivenValidSeasonId_ThenDoesNotThrow()
        {
            //Arrange, Act
            var exception = Record.Exception(() => _movieService.GetMoviesBySeriesId(1));

            //Assert
            Assert.Null(exception);
        }

        [Fact]
        public void GetMoviesBySeriesId_GivenSeriesIdLessThanOne_ThenThrowsArgumentOutOfRangeException()
        {
            //Arrange, Act
            var exception = Record.Exception(() => _movieService.GetMoviesBySeriesId(0));

            //Assert
            Assert.IsType<ArgumentOutOfRangeException>(exception.InnerException);
        }
    }
}