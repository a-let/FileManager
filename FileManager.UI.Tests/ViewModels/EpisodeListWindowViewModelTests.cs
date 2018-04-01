using System;
using FileManager.UI.ViewModels;
using NUnit.Framework;

namespace FileManager.UI.Tests.ViewModels
{
    [TestFixture]
    public class EpisodeListWindowViewModelTests
    {
        [Test]
        public void WhenEpisodeListWindowViewModelIsCalled_ThenDoesNotThrow()
        {
            //Arrange
            //Act & Assert
            Assert.DoesNotThrow(() => new EpisodeListWindowViewModel());
        }
    }
}