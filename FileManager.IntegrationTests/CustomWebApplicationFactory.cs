using FileManager.DataAccessLayer;
using FileManager.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
            SeedShows(db);
            SeedSeasons(db);
            SeedEpisodes(db);
        }

        private void SeedShows(FileManagerContext db)
        {
            db.Show.AddRange(
                new[] 
                {
                    new Show { ShowId = 0, Name = "Test Show", Category = "Testing", Path = @"C:/Temp", Seasons = null }
                });

            db.SaveChanges();
        }

        private void SeedSeasons(FileManagerContext db)
        {
            db.Season.AddRange(
                new[]
                {
                    new Season { SeasonId = 0, ShowId = 1, SeasonNumber = 1, Path = @"C:/Temp", EpisodeList = null }
                });

            db.SaveChanges();
        }     
        
        private void SeedEpisodes(FileManagerContext db)
        {
            db.Episode.AddRange(
                new[]
                {
                    new Episode { EpisodeId = 0, SeasonId = 1, EpisodeNumber = 1, Name = "Test Episode", Format = Models.Constants.FileFormatTypes.MKV, Path = @"C:/Temp" }
                });

            db.SaveChanges();
        }
    }
}