using FileManager.DataAccessLayer;
using FileManager.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace FileManager.IntegrationTests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<Web.Startup>
    {
        private FileManagerContext _fileManagerContext;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkSqlServer()
                    .BuildServiceProvider();

                services.AddDbContext<FileManagerContext>(options =>
                {
                    options.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=FileManagerIntegrationTests;Trusted_Connection=True;");
                    options.UseInternalServiceProvider(serviceProvider);
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    _fileManagerContext = scopedServices.GetRequiredService<FileManagerContext>();

                    _fileManagerContext.Database.EnsureDeleted();
                    _fileManagerContext.Database.EnsureCreated();

                    InitializeDbForTests(_fileManagerContext);
                }
            });
        }

        private void InitializeDbForTests(FileManagerContext db)
        {
            db.Show.AddRange(SeedShows());
            db.SaveChanges();
        }

        private IEnumerable<Show> SeedShows() =>
            new[] { new Show { ShowId = 0, Name = "Test Show", Category = "Testing", Path = @"C:/Temp", Seasons = null } };
    }
}