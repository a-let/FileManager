using FileManager.DataAccessLayer;
using FileManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;
using System.Threading.Tasks;

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

            _context.Database.EnsureCreatedAsync();

            InitializeDbForTestsAsync();
        }

        private async Task InitializeDbForTestsAsync()
        {
            await SeedUsersAsync();
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

            await _context.SaveChangesAsync();
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