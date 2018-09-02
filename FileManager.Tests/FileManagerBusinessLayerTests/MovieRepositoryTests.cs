using Xunit;

using FileManager.BusinessLayer.Repositories;
using FileManager.Models;
using FileManager.Tests.Mocks;
using FileManager.Models.Constants;

namespace FileManager.Tests.FileManagerBusinessLayerTests
{
    public class MovieRepositoryTests
    {
        private readonly MovieRepository _movieRepository = new MovieRepository(new MockFileManagerDb(typeof(Movie)));

        [Fact]
        public void Get_ThenMovieListIsNotEmpty()
        {
            //Arrange, Act
            var movies = _movieRepository.Get();

            //Assert
            Assert.NotEmpty(movies);
        }

        [Fact]
        public void GetById_GivenValidId_ThenMovieIsNotNull()
        {
            //Arrange
            var id = 1;

            //Act
            var movie = _movieRepository.GetById(id);

            //Assert
            Assert.NotNull(movie);
        }

        [Fact]
        public void GetByName_GivenValidName_ThenMovieIsNotNull()
        {
            //Arrange
            var name = "Test Movie Name";

            //Act
            var movie = _movieRepository.GetByName(name);

            //Assert
            Assert.NotNull(movie);
        }

        [Fact]
        public void GetbyParentId_GivenValidId_ThenMovieListIsNotEmpty()
        {
            //Arrange
            var id = 1;

            //Act
            var movies = _movieRepository.GetByParentId(id);

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
                Format = FileFormatTypes.MP4,
                Category = "Test",
                Path = "Test"
            };

            //Act
            var success = _movieRepository.Save(movie);

            //Assert
            Assert.True(success);
        }

        [Fact]
        public void Save_GivenInvalidMovie_ThenReturnsFalse()
        {
            //Arrange
            var movie = new Movie
            {
                Format = FileFormatTypes.MP4,
                Category = "Test"
            };

            //Act
            var success = _movieRepository.Save(movie);

            //Assert
            Assert.False(success);
        }
    }
}