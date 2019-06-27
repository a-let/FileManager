using FileManager.DataAccessLayer;
using FileManager.Models;
using FileManager.Models.Constants;

using System.Text;
using System.Threading.Tasks;

namespace FileManager.Tests.FileManagerDataAccessLayerTests
{
    public static class FileManagerContextExtensions
    {
        public static async Task SeedEpisodesAsync(this FileManagerContext context)
        {
            await context.Episode.AddRangeAsync(new[]
            {
                new Episode
                {
                    EpisodeId = 0,
                    EpisodeNumber = 1,
                    SeasonId = 1,
                    Format = FileFormatTypes.MKV,
                    Name = "Test",
                    Path = "Some Path"
                }
            });
        }

        public static async Task SeedSeasonsAsync(this FileManagerContext context)
        {
            await context.Season.AddRangeAsync(new[]
            {
                new Season
                {
                    SeasonId = 0,
                    ShowId = 1,
                    SeasonNumber = 1,
                    Path = "Test"
                }
            });
        }

        public static async Task SeedShowsAsync(this FileManagerContext context)
        {
            await context.Show.AddRangeAsync(new[]
            {
                new Show
                {
                    ShowId = 0,
                    Name = "Test",
                    Category = "Test",
                    Path = "Some Path"
                }
            });
        }

        public static async Task SeedSeriesAsync(this FileManagerContext context)
        {
            await context.Series.AddRangeAsync(new[]
            {
                new Series
                {
                    SeriesId = 0,
                    Name = "Test",
                    Path = "Some Path"
                }
            });
        }

        public static async Task SeedMoviesAsync(this FileManagerContext context)
        {
            await context.Movie.AddRangeAsync(new[]
            {
                new Movie
                {
                    MovieId = 0,
                    SeriesId = 1,
                    Category = "Test",
                    IsSeries = false,
                    Format = FileFormatTypes.MKV,
                    Name = "Test",
                    Path = "Some Path"
                }
            });
        }

        public static async Task SeedUsersAsync(this FileManagerContext context)
        {
            await context.User.AddRangeAsync(new[]
            {
                new User
                {
                    UserId = 0,
                    FirstName = "Test",
                    LastName = "Tester",
                    UserName = "TTester",
                    PasswordHash = Encoding.ASCII.GetBytes("TestHash"),
                    PasswordSalt = Encoding.ASCII.GetBytes("TestSalt")
                }
            });
        }
    }
}