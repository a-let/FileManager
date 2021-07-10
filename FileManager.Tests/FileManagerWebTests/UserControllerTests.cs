using FileManager.Models.Dtos;
using FileManager.Web.Controllers;
using FileManager.Web.Services.Interfaces;

using NSubstitute;

using System.Collections.Generic;

using Xunit;

namespace FileManager.Tests.FileManagerWebTests
{
    public class UserControllerTests
    {
        private readonly IControllerService<UserDto> _userControllerService;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly UserController _userController;

        public UserControllerTests()
        {
            _userControllerService = Substitute.For<IControllerService<UserDto>>();
            _tokenGenerator = Substitute.For<ITokenGenerator>();
            _userController = new UserController(_userControllerService, _tokenGenerator);
        }

        [Fact]
        public void Get_ReturnsUserDtoCollection()
        {
            // Arrange
            _userControllerService.Get()
                .Returns(new List<UserDto> { new UserDto() });

            // Act
            var result = _userController.Get();
            var users = result.GetValue();

            // Assert
            Assert.NotEmpty(users);
        }
    }
}