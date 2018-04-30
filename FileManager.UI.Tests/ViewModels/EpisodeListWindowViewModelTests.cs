using FileManager.BusinessLayer;
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
            Assert.DoesNotThrow(() => new EpisodeListWindowViewModel((vm) => { }, (title) => { }, (message, caption) => { }));
        }

        [Test]
        public void WhenEpisodeListWindowViewModelIsCalled_GivenEpisodeList_ThenEpisodeListIsNotEmpty()
        {
            //Arrange

            //Act
            var viewModel = new EpisodeListWindowViewModel((vm) => { }, (title) => { }, (message, caption) => { });

            //Assert
            CollectionAssert.IsNotEmpty(viewModel.EpisodeList);
        }

        [Test]
        public void WhenAddNewEpisodeCommandIsCalled_ThenOpenWindowIsInvoked()
        {
            //Arrange
            var isInvoked = false;
            var viewModel = new EpisodeListWindowViewModel((vm) => { isInvoked = true; }, (title) => { }, (message, caption) => { });

            //Act
            viewModel.AddNewEpisodeCommand.Execute(new EpisodeMaintenanceWindowViewModel(null, (title) => { }, (message, caption) => { }));

            //Assert
            Assert.IsTrue(isInvoked);
        }

        [Test]
        public void WhenDoubleClickEpisodeCommandIsCalled_GivenNullEpisode_ThenDoesNotThrow()
        {
            //Arrange
            var viewModel = new EpisodeListWindowViewModel((vm) => { }, (title) => { }, (message, caption) => { });

            //Act & Assert
            Assert.DoesNotThrow(() => viewModel.DoubleClickEpisodeCommand.Execute(null));
        }

        [Test]
        public void WhenDoubleClickEpisodeCommandIsCalled_ThenOpenWindowIsInvoked()
        {
            //Arrange
            var isInvoked = false;
            var viewModel = new EpisodeListWindowViewModel((vm) => { isInvoked = true; }, (title) => { }, (message, caption) => { });

            //Act
            viewModel.DoubleClickEpisodeCommand.Execute(new Episode());

            //Assert
            Assert.IsTrue(isInvoked);
        }
    }
}