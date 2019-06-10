﻿using FileManager.Models;
using FileManager.Tests.Mocks;
using FileManager.Web.Controllers;
using FileManager.Models.Constants;

using System.Collections.Generic;

using Xunit;

namespace FileManager.Tests.FileManagerWebTests
{
    public class MovieControllerTests
    {
        private readonly MovieController _movieController = new MovieController(new MockMovieControllerService(), new MockLoggerService());

        [Fact]
        public void Get_GivenNoParameter_ThenReturnsListOfMovies()
        {
            //Arrange

            //Act
            var movies = _movieController.Get().GetValue();

            //Assert
            Assert.IsAssignableFrom<IEnumerable<Movie>>(movies);
        }

        [Fact]
        public void Get_GivenId_ThenMovieIsReturned()
        {
            //Arrange
            var id = 1;

            //Act
            var movie = _movieController.GetById(id).GetValue();

            //Assert
            Assert.Equal(id, movie.MovieId);
        }

        [Fact]
        public void Get_GivenName_ThenMovieIsReturned()
        {
            //Arrange
            var name = "Test Movie";

            //Act
            var movie = _movieController.GetByName(name).GetValue();

            //Assert
            Assert.Equal(name, movie.Name);
        }

        [Fact]
        public void Get_GivenSeriesId_ThenReturnsListOfMovies()
        {
            //Arrange
            var seriesId = 1;

            //Act
            var movies = _movieController.GetBySeriesId(seriesId).GetValue();

            //Assert
            Assert.IsAssignableFrom<IEnumerable<Movie>>(movies);
        }

        [Fact]
        public void Save_GivenValidMovie_ThenDoesNotThrow()
        {
            //Arrange
            var movie = new Movie
            {
                MovieId = 1,
                SeriesId = 1,
                Name = "Test",
                IsSeries = true,
                Format = FileFormatTypes.MP4,
                Category = "Test",
                Path = "Test"
            };

            //Act
            var exception = Record.Exception(() => _movieController.Post(movie).GetValue());

            //Assert
            Assert.Null(exception);
        }
    }
}