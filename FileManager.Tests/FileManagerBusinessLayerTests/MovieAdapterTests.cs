using Xunit;

using FileManager.BusinessLayer.Adapters;
using FileManager.BusinessLayer.Interfaces;
using FileManager.Models;
using FileManager.Tests.Mocks;

namespace FileManager.Tests.FileManagerBusinessLayerTests
{
    public class MovieAdapterTests
    {
        private readonly IFileManagerObjectAdapter<Movie> _movieAdapter = new MovieAdapter(new MockFileManagerDb(typeof(Movie)));

        [Fact]
        public void Get_ThenMovieListIsNotEmpty()
        {
            //Arrange, Act
            var movies = _movieAdapter.Get();

            //Assert
            Assert.NotEmpty(movies);
        }

        [Fact]
        public void GetById_GivenValidId_ThenMovieIsNotNull()
        {
            //Arrange
            var id = 1;

            //Act
            var movie = _movieAdapter.GetById(id);

            //Assert
            Assert.NotNull(movie);
        }

        [Fact]
        public void GetByName_GivenValidName_ThenMovieIsNotNull()
        {
            //Arrange
            var name = "Test Movie Name";

            //Act
            var movie = _movieAdapter.GetByName(name);

            //Assert
            Assert.NotNull(movie);
        }

        [Fact]
        public void GetbyParentId_GivenValidId_ThenMovieListIsNotEmpty()
        {
            //Arrange
            var id = 1;

            //Act
            var movies = _movieAdapter.GetByParentId(id);

            //Assert
            Assert.NotEmpty(movies);
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
            var success = _movieAdapter.Save(movie);

            //Assert
            Assert.True(success);
        }

        [Fact]
        public void Save_GivenInvalidMovie_ThenReturnsFalse()
        {
            //Arrange
            var movie = new Movie
            {
                Format = "Test",
                Category = "Test"
            };

            //Act
            var success = _movieAdapter.Save(movie);

            //Assert
            Assert.False(success);
        }
    }
}