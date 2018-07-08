﻿using System.Collections.Generic;

using Xunit;

using FileManager.Models;
using FileManager.Tests.Mocks;
using FileManager.Web.Controllers;

namespace FileManager.Tests.FileManagerWebTests
{
    public class ShowControllerTests
    {
        private readonly ShowController _showController = new ShowController(new MockShowControllerService());

        [Fact]
        public void Get_GivenNoParameter_ThenReturnsListOfShows()
        {
            //Arrange

            //Act
            var shows = _showController.Get();

            //Assert
            Assert.IsAssignableFrom<IEnumerable<Show>>(shows);
        }

        [Fact]
        public void Get_GivenId_ThenShowIsReturned()
        {
            //Arrange
            var id = 1;

            //Act
            var show = _showController.Get(id);

            //Assert
            Assert.Equal(id, show.ShowId);
        }

        [Fact]
        public void Get_GivenName_ThenShowIsReturned()
        {
            //Arrange
            var name = "Test Show";

            //Act
            var show = _showController.Get(name);

            //Assert
            Assert.Equal(name, show.Name);
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
            var success = _showController.Post(show);

            //Assert
            Assert.True(success);
        }
    }
}