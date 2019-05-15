using FileManager.DataAccessLayer.Repositories;
using FileManager.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace FileManager.Tests.FileManagerDataAccessLayerTests
{
    [Collection("Database collection")]
    public class ShowRepositoryTests
    {
        private readonly ShowRepository _showRepository;

        public ShowRepositoryTests(DatabaseFixture dbFixture)
        {
            _showRepository = dbFixture.ShowRepository;
        }

        [Fact]
        public void GetShowById_GivenValidId_ThenShowIsReturned()
        {
            //Arrange
            var id = 1;

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
        public void SaveShow_GivenValidNewShow_ThenReturnsShowId()
        {
            //Arrange
            var show = new Show
            {
                ShowId = 0,
                Name = "Test 2",
                Category = "Test",
                Path = "Some other path"
            };

            //Act
            var showId = _showRepository.SaveShow(show);

            //Assert
            Assert.True(showId > 0);
        }

        [Fact(Skip = "Test is just saving a new record. Fix after DAL refactor.")]
        public void SaveShow_GivenValidExistingShow_ThenShowIdIsEqual()
        {
            //Arrange
            var show = new Show
            {
                ShowId = 1,
                Name = "Test",
                Category = "Test",
                Path = "Some Path"
            };

            //Act
            show.Path = "Updated Path";

            var showId = _showRepository.SaveShow(show);

            //Assert
            Assert.Equal(show.ShowId, showId);
        }

        [Fact]
        public void SaveShow_GivenNonExistingShow_ThenThrowsArgumentNullException()
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
            var exception = Record.Exception(() => _showRepository.SaveShow(show));

            //Assert
            Assert.IsType<ArgumentNullException>(exception);
        }
    }
}