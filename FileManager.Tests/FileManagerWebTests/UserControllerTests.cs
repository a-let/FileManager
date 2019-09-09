using FileManager.Tests.Mocks;
using FileManager.Web.Controllers;

using System.Threading.Tasks;

using Xunit;

namespace FileManager.Tests.FileManagerWebTests
{
    public class UserControllerTests
    {
        private readonly UserController _userController = new UserController(new MockUserControllerService(), null);

        [Fact]
        public async Task Get_ReturnsUserDtoCollection()
        {
            // Arrange, Act
            var result = _userController.Get();
            var users = (await result).GetValue();

            // Assert
            Assert.NotEmpty(users);
        }
    }
}