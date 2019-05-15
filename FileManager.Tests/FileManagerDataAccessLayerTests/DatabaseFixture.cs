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
        private readonly FileManagerContext Context;

        public readonly UserRepository UserRepo;
        public readonly EpisodeRepository EpisodeRepo;
        public readonly MovieRepository MovieRepo;

        public DatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<FileManagerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            Context = new FileManagerContext(options);

            Context.Database.EnsureCreated();

            Task.Run(async () => await InitializeDbForTestsAsync());

            UserRepo = new UserRepository(Context);
            EpisodeRepo = new EpisodeRepository(Context);
            MovieRepo = new MovieRepository(Context);
        }

        private async Task InitializeDbForTestsAsync()
        {
            await SeedUsersAsync();
            await SeedEpisodesAsync();
            await SeedMoviesAsync();

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