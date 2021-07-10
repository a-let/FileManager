using FileManager.Models;
using FileManager.Models.Constants;
using FileManager.Web.Controllers;
using FileManager.Web.Services.Interfaces;

using NSubstitute;

using System.Collections.Generic;
using System.Threading.Tasks;

using Xunit;

namespace FileManager.Tests.FileManagerWebTests
{
    public class MovieControllerTests
    {
        private readonly IControllerService<Movie> _movieControllerService;
        private readonly MovieController _movieController;

        public MovieControllerTests()
        {
            _movieControllerService = Substitute.For<IControllerService<Movie>>();
            _movieController = new MovieController(_movieControllerService);
        }

        [Fact]
        public void Get_GivenNoParameter_ThenReturnsListOfMovies()
        {
            // Arrange
            _movieControllerService.Get()
                .Returns(new List<Movie>());

            // Act
            var movies = _movieController.Get().GetValue();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Movie>>(movies);
        }

        [Fact]
        public async Task Get_GivenId_ThenMovieIsReturned()
        {
            // Arrange
            var id = 1;

            _movieControllerService.GetByIdAsync(Arg.Any<int>())
                .Returns(new Movie { MovieId = id });

            // Act
            var movie = (await _movieController.GetById(id)).GetValue();

            // Assert
            Assert.Equal(id, movie.MovieId);
        }

        [Fact]
        public void Get_GivenName_ThenMovieIsReturned()
        {
            // Arrange
            var name = "Test Movie";

            _movieControllerService.GetByName(Arg.Any<string>())
                .Returns(new Movie { Name = name });

            // Act
            var movie = _movieController.GetByName(name).GetValue();

            // Assert
            Assert.Equal(name, movie.Name);
        }

        [Fact]
        public async Task Save_GivenValidMovie_ThenDoesNotThrow()
        {
            // Arrange
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

            _movieControllerService.SaveAsync(Arg.Any<Movie>())
                .Returns(Task.CompletedTask);

            // Act
            var exception = await Record.ExceptionAsync(async () => (await _movieController.Post(movie)).GetValue());

            // Assert
            Assert.Null(exception);
        }
    }
}