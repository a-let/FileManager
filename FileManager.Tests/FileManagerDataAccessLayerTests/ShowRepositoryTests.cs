using FileManager.DataAccessLayer;
using FileManager.DataAccessLayer.Repositories;
using FileManager.Models;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xunit;

namespace FileManager.Tests.FileManagerDataAccessLayerTests
{
    [Collection("Database collection")]
    public class ShowRepositoryTests
    {
        private readonly FileManagerContext _context;

        public ShowRepositoryTests(DatabaseFixture dbFixture)
        {
            _context = dbFixture.Context;
        }

        [Fact]
        public async Task GetShowById_GivenValidId_ThenShowIsReturned()
        {
            // Arrange
            var id = 1;

            var showRepo = new ShowRepository(_context);

            // Act
            var show = await showRepo.GetByIdAsync(id);

            // Assert
            Assert.Equal(id, show.ShowId);
        }

        [Fact]
        public void GetShowByName_GivenValidName_ThenShowIsReturned()
        {
            // Arrange
            var name = "Test";

            var showRepo = new ShowRepository(_context);

            // Act
            var show = showRepo.GetByName(name);

            // Assert
            Assert.IsAssignableFrom<Show>(show);
            Assert.Equal(name, show.Name);
        }

        [Fact]
        public void GetShows_ThenReturnsEpisodeList()
        {
            // Arrange
            var showRepo = new ShowRepository(_context);

            // Act
            var shows = showRepo.Get();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Show>>(shows);
        }

        [Fact]
        public async Task SaveShow_GivenValidNewShow_ThenReturnsShowId()
        {
            // Arrange
            var show = new Show
            {
                ShowId = 0,
                Name = "Test Name",
                Category = "Test",
                Path = "Some other path"
            };

            var showRepo = new ShowRepository(_context);

            // Act
            await showRepo.SaveAsync(show);

            // Assert
            Assert.True(show.ShowId > 0);
        }

        [Fact]
        public async Task SaveShow_GivenValidExistingShow_ThenShowIdIsEqual()
        {
            // Arrange
            var show = new Show
            {
                ShowId = 1,
                Name = "Test",
                Category = "Test",
                Path = "Some Path"
            };

            var showRepo = new ShowRepository(_context);

            // Act
            show.Path = "Updated Path";

            await showRepo.SaveAsync(show);

            // Assert
            Assert.Equal(1, show.ShowId);
        }

        [Fact]
        public async Task SaveShow_GivenNonExistingShow_ThenThrowsArgumentNullException()
        {
            // Arrange
            var show = new Show
            {
                ShowId = 10,
                Name = "Test",
                Category = "Test",
                Path = "Some Path"
            };

            var showRepo = new ShowRepository(_context);

            // Act
            var exception = await Record.ExceptionAsync(async () => await showRepo.SaveAsync(show));

            // Assert
            Assert.IsType<ArgumentNullException>(exception);
        }
    }
}