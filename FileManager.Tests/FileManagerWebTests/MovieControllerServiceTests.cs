using System;
using System.Collections.Generic;

using Xunit;

using FileManager.Models;
using FileManager.Tests.Mocks;
using FileManager.Web.Services;

namespace FileManager.Tests.FileManagerWebTests
{
    public class MovieControllerServiceTests
    {
        private readonly MovieControllerService _movieControllerService = new MovieControllerService(new MockMovieAdapter());

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

            //Act
            var movie = _movieControllerService.GetMovieByName(name);

            //Assert
            Assert.IsAssignableFrom<Movie>(movie);
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
        public void SaveMovie_GivenMovie_ThenReturnsTrue()
        {
            //Arrange
            var movie = new Movie
            {

            };

            //Act
            var success = _movieControllerService.SaveMovie(movie);

            //Assert
            Assert.True(success);
        }
    }
}