using System.Collections.Generic;

using Xunit;

using FileManager.Models;
using FileManager.Tests.Mocks;
using FileManager.Web.Controllers;

namespace FileManager.Tests.FileManagerWebTests
{
    public class MovieControllerTests
    {
        private readonly MovieController _movieController = new MovieController(new MockMovieControllerService());

        [Fact]
        public void Get_GivenNoParameter_ThenReturnsListOfMovies()
        {
            //Arrange

            //Act
            var movies = _movieController.Get();

            //Assert
            Assert.IsAssignableFrom<IEnumerable<Movie>>(movies);
        }

        [Fact]
        public void Get_GivenId_ThenMovieIsReturned()
        {
            //Arrange
            var id = 1;

            //Act
            var movie = _movieController.Get(id);

            //Assert
            Assert.Equal(id, movie.MovieId);
        }

        [Fact]
        public void Get_GivenName_ThenMovieIsReturned()
        {
            //Arrange
            var name = "Test Movie";

            //Act
            var movie = _movieController.Get(name);

            //Assert
            Assert.Equal(name, movie.Name);
        }

        [Fact]
        public void Get_GivenSeriesId_ThenReturnsListOfMovies()
        {
            //Arrange
            var seriesId = 1;

            //Act
            var movies = _movieController.GetBySeriesId(seriesId);

            //Assert
            Assert.IsAssignableFrom<IEnumerable<Movie>>(movies);
        }

        [Fact]
        public void Save_GivenValidMovie_ThenReturnsTrue()
        {
            //Arrange
            var movie = new Movie
            {
                MovieId = 1,
                SeriesId = 1,
                Name = "Test",
                IsSeries = true,
                Format = "Test",
                Category = "Test",
                Path = "Test"
            };

            //Act
            var success = _movieController.Post(movie);

            //Assert
            Assert.True(success);
        }
    }
}