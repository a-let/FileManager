using FileManager.DataAccessLayer;
using FileManager.Models;
using FileManager.Web.Services.Interfaces;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Serilog;
using Serilog.Events;

using System;
using System.Linq;
using System.Reflection;

using Xunit.Abstractions;

namespace FileManager.IntegrationTests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<Web.Startup>
    {
        private readonly IMessageSink _messageSink;

        public CustomWebApplicationFactory(IMessageSink messageSink)
        {
            _messageSink = messageSink;
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Warning()
                .MinimumLevel.Override("FileManager", LogEventLevel.Debug)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("AssemblyName", Assembly.GetExecutingAssembly().GetName().Name)
                .WriteTo.XUnitTestSink(_messageSink)
                .CreateLogger();

            return base.CreateHost(builder);
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            builder.UseConfiguration(configuration);

            builder.ConfigureServices(services =>
            {
                RemoveDbContext(services);

                services.AddDbContext<FileManagerContext>(options =>
                {
                    options.UseSqlServer(configuration["IntegrationTestsConnectionString"]);
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var _fileManagerContext = scopedServices.GetRequiredService<FileManagerContext>();
                    var _logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<Web.Startup>>>();

                    try
                    {
                        var connectionString = _fileManagerContext.Database.GetDbConnection().ConnectionString;
                        if (!connectionString.Contains("IntegrationTests"))
                            throw new InvalidOperationException("Incorrect connection string");

                        _fileManagerContext.Database.EnsureDeleted();
                        _fileManagerContext.Database.EnsureCreated();

                        InitializeDbForTests(_fileManagerContext, scopedServices.GetRequiredService<ICryptographyService>());

                        _logger.LogInformation("Database Initialized");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to create, delete, or init database. ");
                        throw;
                    }
                }
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposing)
                return;

            Log.CloseAndFlush();

            base.Dispose(disposing);
        }

        /// <summary>
        /// Required due to migrating from ASP.NET Core 2.2 to 3.0 which causes the app's Startup.ConfiugreServices
        /// to run AFTER the test apps builder.ConfigureServices.
        /// </summary>
        /// <remarks>
        /// https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-3.0#customize-webapplicationfactory
        /// </remarks>
        /// <param name="services"></param>
        private void RemoveDbContext(IServiceCollection services)
        {
            var descriptor = services
                    .SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<FileManagerContext>));

            services.Remove(descriptor);
        }

        private void InitializeDbForTests(FileManagerContext db, ICryptographyService cryptoService)
        {
            SeedShows(db);
            SeedSeasons(db);
            SeedEpisodes(db);
            SeedSeries(db);
            SeedMovies(db);
            SeedUsers(db, cryptoService);
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

        private void SeedSeries(FileManagerContext db)
        {
            db.Series.AddRange(
                new[]
                {
                    new Series { SeriesId = 0, Name = "Test Series", Path = @"C:/Temp" }
                });

            db.SaveChanges();
        }

        private void SeedMovies(FileManagerContext db)
        {
            db.Movie.AddRange(
                new[]
                {
                    new Movie { MovieId = 0, SeriesId = 1, Category = "Testing", Format = Models.Constants.FileFormatTypes.MKV, IsSeries = true, Name = "Test Movie", Path = @"C:/Temp" }
                });

            db.SaveChanges();
        }

        private void SeedUsers(FileManagerContext db, ICryptographyService cryptoService)
        {
            cryptoService.CreateHash("Test123", out byte[] hash, out byte[] salt);

            db.User.AddRange(
                new[]
                {
                    new User
                    {
                        UserId = 0,
                        FirstName = "John",
                        LastName = "Doe",
                        UserName = "JDoe",
                        PasswordHash = hash,
                        PasswordSalt = salt
                    }
                });
            db.SaveChanges();
        }
    }
}