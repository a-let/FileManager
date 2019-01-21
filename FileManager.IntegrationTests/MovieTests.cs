using FileManager.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FileManager.IntegrationTests
{
    [Collection("Integration Test Collection")]
    public class MovieTests : TestBase
    {
        public MovieTests() : base(new CustomWebApplicationFactory<Web.Startup>())
        { }

        [Fact]
        public async Task Get()
        {
            // Arrange

            // Act
            var responseMessage = await _client.GetAsync("api/Movie");
            var strContent = await responseMessage.Content.ReadAsStringAsync();
            var movie = DeserializeObject<IEnumerable<Movie>>(strContent);

            // Assert
            Assert.NotEmpty(movie);
        }

        [Fact]
        public async Task GetById()
        {
            // Arrange
            var movieId = 1;

            // Act
            var responseMessage = await _client.GetAsync($"api/Movie/id/{movieId}");
            var strContent = await responseMessage.Content.ReadAsStringAsync();
            var movies = DeserializeObject<Movie>(strContent);

            // Assert
            Assert.NotNull(movies);
            Assert.Equal(movieId, movies.SeriesId);
        }

        [Fact]
        public async Task GetByName()
        {
            // Arrange
            var movieName = "Test Movie";

            // Act
            var responseMessage = await _client.GetAsync($"api/Movie/name/{movieName}");
            var strContent = await responseMessage.Content.ReadAsStringAsync();
            var movie = DeserializeObject<Movie>(strContent);

            // Assert
            Assert.NotNull(movie);
            Assert.Equal(movieName, movie.Name);
        }

        [Fact]
        public async Task GetBySeriesId()
        {
            // Arrange
            var seriesId = 1;

            // Act
            var responseMessage = await _client.GetAsync($"api/Movie/seriesId/{seriesId}");
            var strContent = await responseMessage.Content.ReadAsStringAsync();
            var movies = DeserializeObject<IEnumerable<Movie>>(strContent);

            // Assert
            Assert.NotEmpty(movies);
            Assert.Equal(seriesId, movies.First().SeriesId);
        }

        [Fact]
        public async Task Post()
        {
            // Arrange
            var movie = new Movie
            {
                MovieId = 0,
                SeriesId = 1,
                Category = "Testing",
                IsSeries = true,
                Name = "Test Episode Two",
                Format = Models.Constants.FileFormatTypes.MKV,
                Path = @"C:/Temp"
            };

            // Act
            var responseMessage = await _client.PostAsync("api/Movie", CreateStringContent(movie));
            var strContent = await responseMessage.Content.ReadAsStringAsync();
            var success = DeserializeObject<bool>(strContent);

            // Assert
            Assert.True(success);
            Assert.Equal(2, GetMovies().Result.Count());
        }

        private async Task<IEnumerable<Movie>> GetMovies()
        {
            var responseMessage = await _client.GetAsync("api/Movie");
            var strContent = await responseMessage.Content.ReadAsStringAsync();
            return DeserializeObject<IEnumerable<Movie>>(strContent);
        }
    }
}