using FileManager.DataAccessLayer.Repositories;
using FileManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FileManager.Tests.FileManagerDataAccessLayerTests
{
    public class UserRepositoryTests : TestBase
    {
        private readonly UserRepository _userRepository;

        public UserRepositoryTests() : base(nameof(UserRepositoryTests))
        {
            _userRepository = new UserRepository(_context);
        }

        [Fact]
        public async Task GetUserByIdAsync_GivenValidId_ThenUserIsReturned()
        {
            // Arrange
            var id = 1;

            // Act
            var user = await _userRepository.GetUserByIdAsync(id);

            // Assert
            Assert.Equal(id, user.UserId);
        }

        [Fact]
        public void GetUserByUserName_GivenValidUserName_ThenUserIsReturned()
        {
            // Arrange
            var userName = "TTester";

            // Act
            var user = _userRepository.GetUserByUserName(userName);

            // Assert
            Assert.Equal(userName, user.UserName);
        }

        [Fact]
        public void GetUsers_ThenUsersAreReturned()
        {
            // Arrange, Act
            var users = _userRepository.GetUsers();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<User>>(users);
            Assert.True(users.Count() == 1);
        }

        [Fact]
        public async Task SaveUser_GivenValidUser_TheReturnsUserId()
        {
            // Arrange
            var user = new User
            {
                UserId = 0,
                FirstName = "John",
                LastName = "Doe",
                UserName = "JDoe",
                PasswordHash = Encoding.ASCII.GetBytes("TestHash1"),
                PasswordSalt = Encoding.ASCII.GetBytes("TestSalt1")
            };

            // Act
            var userId = await _userRepository.SaveUserAsync(user);

            // Assert
            Assert.True(userId > 0);
            Assert.Equal(userId, user.UserId);
        }

        [Fact]
        public async Task SaveUser_GivenExistingUser_ThenUserIdIsEqual()
        {
            // Arrange
            var newUserName = "Tester1";

            var user = new User
            {
                UserId = 1,
                FirstName = "Test",
                LastName = "Tester",
                UserName = "TTester",
                PasswordHash = Encoding.ASCII.GetBytes("TestHash"),
                PasswordSalt = Encoding.ASCII.GetBytes("TestSalt")
            };

            // Act
            user.UserName = newUserName;

            var userId = await _userRepository.SaveUserAsync(user);

            // Assert
            Assert.True(userId > 0);
            Assert.Equal(newUserName, user.UserName);
        }

        [Fact]
        public async Task SaveUser_GivenInvalidUser_ThenThrowsArgumentNullException()
        {
            // Arrange
            var user = new User
            {
                UserId = 10,
                FirstName = "Test",
                LastName = "Tester",
                UserName = "TTester",
                PasswordHash = Encoding.ASCII.GetBytes("TestHash"),
                PasswordSalt = Encoding.ASCII.GetBytes("TestSalt")
            };

            // Act
            var exception = await Record.ExceptionAsync(() => _userRepository.SaveUserAsync(user));

            // Assert
            Assert.IsType<ArgumentNullException>(exception);
        }
    }
}