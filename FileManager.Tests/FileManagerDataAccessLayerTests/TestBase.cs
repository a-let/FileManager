using FileManager.DataAccessLayer;
using FileManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Tests.FileManagerDataAccessLayerTests
{
    [Obsolete("Add seed data to DatabaseFixture.cs and use [Collection(\"Database collection\")]")]
    public abstract class TestBase : IDisposable
    {
        protected readonly FileManagerContext _context;

        public TestBase(string dbName)
        {
            var options = new DbContextOptionsBuilder<FileManagerContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            _context = new FileManagerContext(options);

            Task.Run(async () => await _context.Database.EnsureCreatedAsync());
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