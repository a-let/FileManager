﻿using FileManager.Models.Constants;
using FileManager.Models;
using FileManager.Tests.Mocks;
using FileManager.Web.Services;

using System;
using System.Collections.Generic;

using Xunit;

namespace FileManager.Tests.FileManagerWebTests
{
    public class EpisodeControllerServiceTests
    {
        private readonly EpisodeControllerService _episodeControllerService = new EpisodeControllerService(new MockEpisodeRepository());

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

            //Act
            var episode = _episodeControllerService.GetEpisodeByName(name);

            //Assert
            Assert.IsAssignableFrom<Episode>(episode);
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
        public void SaveEpisode_GivenNewEpisode_ThenReturnsTrue()
        {
            //Arrange
            var episode = new Episode
            {
                EpisodeId = 0,
                SeasonId = 1,
                Format = FileFormatTypes.MKV,
                Name = "Test Name",
                Path = "Some Path"
            };

            //Act
            var success = _episodeControllerService.SaveEpisode(episode);

            //Assert
            Assert.True(success);
        }

        [Fact]
        public void GetEpisodesBySeasonId_GivenInvalidSeasonId_ThenThrowsArgumentException()
        {
            //Arrange
            var id = 0;

            //Act
            var exception = Record.Exception(() => _episodeControllerService.GetEpisodesBySeasonId(id));

            //Assert
            Assert.IsType<ArgumentException>(exception);
        }

        [Fact]
        public void GetEpisodesBySeasonId_GivenValidSeasonId_THenThrowsArgumentException()
        {
            //Arrange
            var id = 1;

            //Act
            var episodes = _episodeControllerService.GetEpisodesBySeasonId(id);

            //Assert
            Assert.IsAssignableFrom<IEnumerable<Episode>>(episodes);
        }
    }
}