using FileManager.DataAccessLayer.Repositories;
using FileManager.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FileManager.Tests.FileManagerDataAccessLayerTests
{
    public class ShowRepositoryTests : TestBase
    {
        private readonly ShowRepository _showRepository;

        public ShowRepositoryTests() : base(nameof(ShowRepositoryTests))
        {
            _showRepository = new ShowRepository(_context);
        }

        [Fact]
        public void GetShowById_GivenValidId_ThenShowIsReturned()
        {
            //Arrange
            var id = 1;

            _context.Shows.Add(new Show
            {
                ShowId = 1,
                Name = "Test",
                Category = "Test",
                Path = "Some Path"
            });

            _context.SaveChanges();

            //Act
            var show = _showRepository.GetShowById(id);

            //Assert
            Assert.Equal(id, show.ShowId);
        }

        [Fact]
        public void GetShowByName_GivenValidName_ThenShowIsReturned()
        {
            //Arrange
            var name = "Test";

            _context.Shows.Add(new Show
            {
                ShowId = 0,
                Name = name,
                Category = "Test",
                Path = "Some Path"
            });

            _context.SaveChanges();

            //Act
            var show = _showRepository.GetShowByName(name);

            //Assert
            Assert.IsAssignableFrom<Show>(show);
            Assert.Equal(name, show.Name);
        }

        [Fact]
        public void GetShows_ThenReturnsEpisodeList()
        {
            //Arrange, Act
            var shows = _showRepository.GetShows();

            //Assert
            Assert.IsAssignableFrom<IEnumerable<Show>>(shows);
        }

        [Fact]
        public void SaveShow_GivenValidNewShow_ThenReturnsTrue()
        {
            //Arrange
            var show = new Show
            {
                ShowId = 0,
                Name = "Test",
                Category = "Test",
                Path = "Some Path"
            };

            //Act
            var success = _showRepository.SaveShow(show);

            //Assert
            Assert.True(success);
        }

        [Fact]
        public void SaveShow_GivenValidExistingShow_ThenReturnsTrue()
        {
            //Arrange
            var show = new Show
            {
                ShowId = 1,
                Name = "Test",
                Category = "Test",
                Path = "Some Path"
            };

            _context.Shows.Add(show);
            _context.SaveChanges();

            //Act

            show.ShowId = 1;
            show.Name = "Updated Name";

            var success = _showRepository.SaveShow(show);

            //Assert
            Assert.True(success);
        }

        [Fact]
        public void SaveShow_GivenNonExistingShow_ThenReturnsFalse()
        {
            //Arrange
            var show = new Show
            {
                ShowId = 10,
                Name = "Test",
                Category = "Test",
                Path = "Some Path"
            };

            //Act
            var success = _showRepository.SaveShow(show);

            //Assert
            Assert.False(success);
        }

        protected override void Dispose(bool disposing = true)
        {
            base.Dispose(disposing);

            _showRepository.Dispose();
        }
    }
}