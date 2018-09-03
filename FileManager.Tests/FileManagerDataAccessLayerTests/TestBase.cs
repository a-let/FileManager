using FileManager.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System;

namespace FileManager.Tests.FileManagerDataAccessLayerTests
{
    public abstract class TestBase : IDisposable
    {
        protected readonly FileManagerContext _context;

        public TestBase(string dbName)
        {
            var options = new DbContextOptionsBuilder<FileManagerContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            _context = new FileManagerContext(options);
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