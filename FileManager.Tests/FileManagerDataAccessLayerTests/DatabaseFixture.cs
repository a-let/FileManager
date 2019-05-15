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
        private readonly FileManagerContext _context;

        public readonly UserRepository UserRepo;
        public readonly EpisodeRepository EpisodeRepo;
        public readonly MovieRepository MovieRepo;
        public readonly SeasonRepository SeasonRepository;
        public readonly SeriesRepository SeriesRepository;
        public readonly ShowRepository ShowRepository;
        public readonly LogRepository LogRepository;

        public DatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<FileManagerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new FileManagerContext(options);

            _context.Database.EnsureCreated();

            Task.Run(async () => await InitializeDbForTestsAsync());

            UserRepo = new UserRepository(_context);
            EpisodeRepo = new EpisodeRepository(_context);
            MovieRepo = new MovieRepository(_context);
            SeasonRepository = new SeasonRepository(_context);
            SeriesRepository = new SeriesRepository(_context);
        }

        private async Task InitializeDbForTestsAsync()
        {
            await SeedUsersAsync();
            await SeedEpisodesAsync();
            await SeedMoviesAsync();
            await SeedSeasonsAsync();
            await SeedSeriesAsync();

            await _context.SaveChangesAsync();
        }

        private async Task SeedEpisodesAsync()
        {
            await _context.Episode.AddRangeAsync(new[]
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
            await _context.Season.AddRangeAsync(new[]
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

        private async Task SeedSeriesAsync()
        {
            await _context.Series.AddRangeAsync(new[]
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
            await _context.Movie.AddRangeAsync(new[]
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
            await _context.User.AddRangeAsync(new[]
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
                _context.Database.EnsureDeleted();
                _context.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}