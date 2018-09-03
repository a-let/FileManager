using FileManager.Models.Constants;
using FileManager.Models;
using FileManager.Web.Services;
using FileManager.DataAccessLayer;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;

using Xunit;

namespace FileManager.Tests.FileManagerWebTests
{
    public class EpisodeControllerServiceTests : IDisposable
    {
        private readonly FileManagerContext _context;
        private readonly EpisodeControllerService _episodeControllerService;

        public EpisodeControllerServiceTests()
        {
            var options = new DbContextOptionsBuilder<FileManagerContext>()
                .UseInMemoryDatabase(databaseName: "EpisodeControllerServiceTests")
                .Options;

            _context = new FileManagerContext(options);

            _episodeControllerService = new EpisodeControllerService(_context);
        }

        [Fact]
        public void GetEpisodeById_GivenInvalidId_ThenThrowsArgumentException()
        {
            //Arrange
            var id = 0;

            //Act
            var exception = Record.Exception(() => _episodeControllerService.GetEpisodeById(id));

            //Assert
            Assert.IsType<ArgumentException>(exception);
        }

        [Fact]
        public void GetEpisodeById_GivenValidId_ThenEpisodeIsReturned()
        {
            //Arrange
            var id = 1;

            _context.Episodes.Add(new Episode
            {
                EpisodeId = id,
                EpisodeNumber = 1,
                SeasonId = 1,
                Format = FileFormatTypes.MKV,
                Name = "Test",
                Path = "Some Path"
            });

            _context.SaveChanges();

            //Act
            var episode = _episodeControllerService.GetEpisodeById(id);

            //Assert
            Assert.IsAssignableFrom<Episode>(episode);
        }

        [Fact]
        public void GetEpisodes_ThenReturnsEpisodeList()
        {
            //Arrange, Act
            var episodes = _episodeControllerService.GetEpisodes();

            //Assert
            Assert.IsAssignableFrom<IEnumerable<Episode>>(episodes);
        }

        [Fact]
        public void GetEpisodeByName_GivenInvalidName_ThenThrowsArgumentNullException()
        {
            //Arrange
            var name = string.Empty;

            //Act
            var exception = Record.Exception(() => _episodeControllerService.GetEpisodeByName(name));

            //Assert
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void GetEpisodeByName_GivenValidName_ThenEpisodeIsReturned()
        {
            //Arrange
            var name = "Test";

            _context.Episodes.Add(new Episode
            {
                EpisodeNumber = 1,
                SeasonId = 1,
                Format = FileFormatTypes.MKV,
                Name = name,
                Path = "Some Path"
            });

            _context.SaveChanges();

            //Act
            var episode = _episodeControllerService.GetEpisodeByName(name);

            //Assert
            Assert.IsAssignableFrom<Episode>(episode);
            Assert.Equal(name, episode.Name);
        }

        [Fact]
        public void SaveEpisode_GivenNullEpisode_ThenThrowsArgumentNullReferenceException()
        {
            //Arrange
            Episode episode = null;

            //Act
            var exception = Record.Exception(() => _episodeControllerService.SaveEpisode(episode));

            //Assert
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void SaveEpisode_GivenNewEpisode_ThenDoesNotThrow()
        {
            //Arrange
            var episode = new Episode
            {
                EpisodeNumber = 0,
                SeasonId = 1,
                Format = FileFormatTypes.MKV,
                Name = "Test Name",
                Path = "Some Path"
            };

            //Act
            var exception = Record.Exception(() => _episodeControllerService.SaveEpisode(episode));

            //Assert
            Assert.Null(exception);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}