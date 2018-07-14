using Xunit;

using FileManager.Models;
using FileManager.Tests.Mocks;
using FileManager.BusinessLayer.Repositories;

namespace FileManager.Tests.FileManagerBusinessLayerTests
{
    public class SeriesRepositoryTests
    {
        private readonly SeriesRepository _seriesRepository = new SeriesRepository(new MockFileManagerDb(typeof(Series)));

        [Fact]
        public void GetById_GivenValidId_ThenSeriesIsNotNull()
        {
            //Arrange
            var id = 1;

            //Act
            var series = _seriesRepository.GetById(id);

            //Assert
            Assert.NotNull(series);
        }

        [Fact]
        public void Get_ThenSeriesListIsNotEmpty()
        {
            //Arrange, Act
            var seriesList = _seriesRepository.Get();

            //Assert
            Assert.NotEmpty(seriesList);
        }

        [Fact]
        public void GetByName_GivenValidName_ThenSeriesIsNotNull()
        {
            //Arrange
            var name = "Test Name";

            //Act
            var series = _seriesRepository.GetByName(name);

            //Assert
            Assert.NotNull(series);
        }

        [Fact]
        public void Save_GivenValidSeries_ThenReturnsTrue()
        {
            //Arrange
            var series = new Series
            {
                SeriesId = 1,
                Name = "Test",
                Path = "Test"
            };

            //Act
            var success = _seriesRepository.Save(series);

            //Assert
            Assert.True(success);
        }

        [Fact]
        public void Save_GivenInvalidSeries_ThenReturnsFalse()
        {
            //Arrange
            var series = new Series();

            //Act
            var success = _seriesRepository.Save(series);

            //Assert
            Assert.False(success);
        }
    }
}