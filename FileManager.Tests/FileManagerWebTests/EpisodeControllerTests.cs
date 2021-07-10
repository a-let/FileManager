using FileManager.Models;
using FileManager.Models.Constants;
using FileManager.Web.Controllers;
using FileManager.Web.Services.Interfaces;

using NSubstitute;

using System.Collections.Generic;
using System.Threading.Tasks;

using Xunit;

namespace FileManager.Tests.FileManagerWebTests
{
    public class EpisodeControllerTests
    {
        private readonly IControllerService<Episode> _episodeControllerService;
        private readonly EpisodeController _episodeController;

        public EpisodeControllerTests()
        {
            _episodeControllerService = Substitute.For<IControllerService<Episode>>();
            _episodeController = new EpisodeController(_episodeControllerService);
        }

        [Fact]
        public void Get_GivenNoParameter_ThenReturnsListOfEpisodes()
        {
            // Arrange

            // Act
            var episodes = _episodeController.Get().GetValue();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Episode>>(episodes);
        }

        [Fact]
        public async Task Get_GivenId_ThenEpisodeIsReturned()
        {
            // Arrange
            var id = 1;

            _episodeControllerService.GetByIdAsync(Arg.Any<int>())
                .Returns(new Episode { EpisodeId = id });

            // Act
            var episode = (await _episodeController.GetByIdAsync(id)).GetValue();

            // Assert
            Assert.Equal(id, episode.EpisodeId);
        }

        [Fact]
        public void Get_GivenName_ThenEpisodeIsReturned()
        {
            // Arrange
            var name = "Test Episode";

            _episodeControllerService.GetByName(Arg.Any<string>())
                .Returns(new Episode { Name = name });

            // Act
            var episode = _episodeController.GetByName(name).GetValue();

            // Assert
            Assert.Equal(name, episode.Name);
        }

        [Fact]
        public async Task Save_GivenValidEpisode_ThenReturnsOne()
        {
            // Arrange
            var episode = new Episode
            {
                EpisodeId = 1,
                SeasonId = 1,
                Name = "Test",
                EpisodeNumber = 1,
                Format = FileFormatTypes.MP4,
                Path = "Test"
            };

            _episodeControllerService.SaveAsync(Arg.Any<Episode>())
                .Returns(Task.CompletedTask);

            // Act
            episode = (await _episodeController.PostAsync(episode)).GetValue();

            // Assert
            Assert.Equal(1, episode.EpisodeId);
        }
    }
}