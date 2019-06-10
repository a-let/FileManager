using System.Collections.Generic;

using Xunit;

using FileManager.Models;
using FileManager.Tests.Mocks;
using FileManager.Web.Controllers;
using FileManager.Models.Constants;

namespace FileManager.Tests.FileManagerWebTests
{
    public class EpisodeControllerTests
    {
        private readonly EpisodeController _episodeController = new EpisodeController(new MockEpisodeControllerService(), new MockLoggerService());

        [Fact]
        public void Get_GivenNoParameter_ThenReturnsListOfEpisodes()
        {
            //Arrange

            //Act
            var episodes = _episodeController.Get().GetValue();

            //Assert
            Assert.IsAssignableFrom<IEnumerable<Episode>>(episodes);
        }

        [Fact]
        public void Get_GivenId_ThenEpisodeIsReturned()
        {
            //Arrange
            var id = 1;

            //Act
            var episode = _episodeController.GetById(id).GetValue();

            //Assert
            Assert.Equal(id, episode.EpisodeId);
        }

        [Fact]
        public void Get_GivenName_ThenEpisodeIsReturned()
        {
            //Arrange
            var name = "Test Episode";

            //Act
            var episode = _episodeController.GetByName(name).GetValue();

            //Assert
            Assert.Equal(name, episode.Name);
        }
        
        [Fact]
        public void Get_GivenSeasonId_ThenReturnsListOfEpisodes()
        {
            //Arrange
            var seasonId = 1;

            //Act
            var episodes = _episodeController.GetBySeasonId(seasonId).GetValue();

            //Assert
            Assert.IsAssignableFrom<IEnumerable<Episode>>(episodes);
        }

        [Fact]
        public void Save_GivenValidEpisode_ThenReturnsOne()
        {
            //Arrange
            var episode = new Episode
            {
                EpisodeId = 1,
                SeasonId = 1,
                Name = "Test",
                EpisodeNumber = 1,
                Format = FileFormatTypes.MP4,
                Path = "Test"
            };

            //Act
            var episodeId = _episodeController.Post(episode).GetValue();

            //Assert
            Assert.Equal(1, episodeId);
        }
    }
}