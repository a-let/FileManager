using Xunit;

using FileManager.BusinessLayer.Adapters;
using FileManager.BusinessLayer.Interfaces;
using FileManager.Models;
using FileManager.Tests.Mocks;

namespace FileManager.Tests.FileManagerBusinessLayerTests
{
    public class SeriesAdapterTests
    {
        private readonly IFileManagerObjectAdapter<Series> _seriesAdapter = new SeriesAdapter(new MockFileManagerDb(typeof(Series)));

        [Fact]
        public void GetById_GivenValidId_ThenSeriesIsNotNull()
        {
            //Arrange
            var id = 1;

            //Act
            var series = _seriesAdapter.GetById(id);

            //Assert
            Assert.NotNull(series);
        }

        [Fact]
        public void Get_ThenSeriesListIsNotEmpty()
        {
            //Arrange, Act
            var seriesList = _seriesAdapter.Get();

            //Assert
            Assert.NotEmpty(seriesList);
        }

        [Fact]
        public void GetByName_GivenValidName_ThenSeriesIsNotNull()
        {
            //Arrange
            var name = "Test Name";

            //Act
            var series = _seriesAdapter.GetByName(name);

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
            var success = _seriesAdapter.Save(series);

            //Assert
            Assert.True(success);
        }

        [Fact]
        public void Save_GivenInvalidSeries_ThenReturnsFalse()
        {
            //Arrange
            var series = new Series();

            //Act
            var success = _seriesAdapter.Save(series);

            //Assert
            Assert.False(success);
        }
    }
}