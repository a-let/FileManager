using FileManager.DataAccessLayer;
using FileManager.DataAccessLayer.Repositories;
using FileManager.Models;
using FileManager.Models.Constants;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Tests.FileManagerDataAccessLayerTests
{
    public class DatabaseFixture : IDisposable
    {
        public readonly FileManagerContext Context;

        public DatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<FileManagerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            Context = new FileManagerContext(options);
            Context.Database.EnsureCreated();

            InitializeDbForTestsAsync().GetAwaiter().GetResult();
        }

        private async Task InitializeDbForTestsAsync()
        {
            await SeedUsersAsync();
            await SeedEpisodesAsync();
            await SeedMoviesAsync();
            await SeedSeasonsAsync();
            await SeedSeriesAsync();
            await SeedShowsAsync();

            await Context.SaveChangesAsync();
        }

        private async Task SeedEpisodesAsync()
        {
            await Context.Episode.AddRangeAsync(new[]
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

        private async Task SeedSeasonsAsync()
        {
            await Context.Season.AddRangeAsync(new[]
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

        private async Task SeedShowsAsync()
        {
            await Context.Show.AddRangeAsync(new[]
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

        private async Task SeedSeriesAsync()
        {
            await Context.Series.AddRangeAsync(new[]
            {
                new Series
                {
                    SeriesId = 0,
                    Name = "Test",
                    Path = "Some Path"
                }
            });
        }

        private async Task SeedMoviesAsync()
        {
            await Context.Movie.AddRangeAsync(new[]
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

        private async Task SeedUsersAsync()
        {
            await Context.User.AddRangeAsync(new[]
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

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Context.Database.EnsureDeleted();
                Context.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}