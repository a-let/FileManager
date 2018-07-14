using Xunit;

using FileManager.BusinessLayer.Repositories;
using FileManager.Models;
using FileManager.Tests.Mocks;

namespace FileManager.Tests.FileManagerBusinessLayerTests
{
    public class ShowRepositoryTests
    {
        private readonly ShowRepository _showRepository = new ShowRepository(new MockFileManagerDb(typeof(Show)));

        [Fact]
        public void GetById_GivenValidId_ThenShowIsNotNull()
        {
            //Arrange
            var id = 1;

            //Act
            var show = _showRepository.GetById(id);

            //Assert
            Assert.NotNull(show);
        }

        [Fact]
        public void Get_ThenShowListIsNotEmpty()
        {
            //Arrange, Act
            var showList = _showRepository.Get();

            //Assert
            Assert.NotEmpty(showList);
        }

        [Fact]
        public void GetByName_GivenValidName_ThenShowIsNotNull()
        {
            //Arrange
            var name = "Test Name";

            //Act
            var show = _showRepository.GetByName(name);

            //Assert
            Assert.NotNull(show);
        }

        [Fact]
        public void Save_GivenValidShow_ThenReturnsTrue()
        {
            //Arrange
            var show = new Show
            {
                ShowId = 1,
                Name = "Test",
                Category = "Test",
                Path = "Test"
            };

            //Act
            var success = _showRepository.Save(show);

            //Assert
            Assert.True(success);
        }

        [Fact]
        public void Save_GivenInvalidShow_ThenReturnsFalse()
        {
            //Arrange
            var show = new Show();

            //Act
            var success = _showRepository.Save(show);

            //Assert
            Assert.False(success);
        }
    }
}