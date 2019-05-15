using FileManager.Tests.Mocks;
using FileManager.Web.Controllers;

using Xunit;

namespace FileManager.Tests.FileManagerWebTests
{
    public class UserControllerTests
    {
        private readonly UserController _userController = new UserController(new MockUserControllerService(), new MockLoggerService(), new MockConfiguration(), null);

        [Fact]
        public void Get_ReturnsUserDtoCollection()
        {
            // Arrange, Act
            var result = _userController.Get();
            var users = result.GetValue();

            // Assert
            Assert.NotEmpty(users);
        }
    }
}