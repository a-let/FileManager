using System;
using System.Collections.Generic;

using Xunit;

using FileManager.Models;
using FileManager.Web.Controllers;
using FileManager.Web.Services;
using FileManager.BusinessLayer.Helpers;

namespace FileManager.Web.Tests.ControllerTests
{
    public class EpisodeControllerTests
    {
        private readonly EpisodeController _episodeController = new EpisodeController(new MockEpisodeContollerService());

        [Fact]
        public void Get_GivenNoParameter_ThenReturnsListOfEpisodes()
        {
            //Arrange

            //Act
            var episodes = _episodeController.Get();

            //Assert
            Assert.IsAssignableFrom<IEnumerable<Episode>>(episodes);
        }

        [Fact]
        public void Get_GivenId_ThenEpisodeIsReturned()
        {
            //Arrange
            var id = 1;

            //Act
            var episode = _episodeController.Get(id);

            //Assert
            Assert.Equal(id, episode.EpisodeId);
        }

        [Fact]
        public void Get_GivenName_ThenEpisodeIsReturned()
        {
            //Arrange
            var name = "Test Episode";

            //Act
            var episode = _episodeController.Get(name);

            //Assert
            Assert.Equal(name, episode.Name);
        }
    }

    public class MockEpisodeContollerService : IEpisodeControllerService
    {
        public Episode GetEpisodeById(int id)
        {
            return id != 1 ? null : new Episode
            {
                EpisodeId = 1
            };
        }

        public Episode GetEpisodeByName(string name)
        {
            return string.IsNullOrWhiteSpace(name) ? null : new Episode
            {
                Name = "Test Episode"
            };
        }

        public IEnumerable<Episode> GetEpisodes()
        {
            return new List<Episode>();
        }

        public IEnumerable<Episode> GetEpisodesBySeasonId(int seasonId)
        {
            throw new NotImplementedException();
        }

        public bool SaveEpisode(Episode episode)
        {
            throw new NotImplementedException();
        }
    }
}