using FileManager.DataAccessLayer;

using Microsoft.EntityFrameworkCore;

using System;
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
            await Context.SeedUsersAsync();
            await Context.SeedEpisodesAsync();
            await Context.SeedMoviesAsync();
            await Context.SeedSeasonsAsync();
            await Context.SeedSeriesAsync();
            await Context.SeedShowsAsync();

            await Context.SaveChangesAsync();
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