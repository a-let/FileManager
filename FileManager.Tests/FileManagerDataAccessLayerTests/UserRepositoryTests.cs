using FileManager.DataAccessLayer;
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
    [Collection("Database collection")]
    public class UserRepositoryTests
    {
        private readonly FileManagerContext _context;

        public UserRepositoryTests(DatabaseFixture dbFixture)
        {
            _context = dbFixture.Context;
        }

        [Fact]
        public async Task GetUserByIdAsync_GivenValidId_ThenUserIsReturned()
        {
            // Arrange
            var id = 1;

            var userRepo = new UserRepository(_context);

            // Act
            var user = await userRepo.GetByIdAsync(id);

            // Assert
            Assert.Equal(id, user.UserId);
        }

        [Fact]
        public void GetUserByUserName_GivenValidUserName_ThenUserIsReturned()
        {
            // Arrange
            var userName = "TTester";

            var userRepo = new UserRepository(_context);

            // Act
            var user = userRepo.GetByName(userName);

            // Assert
            Assert.Equal(userName, user.UserName);
        }

        [Fact]
        public void GetUsers_ThenUsersAreReturned()
        {
            // Arrange
            var userRepo = new UserRepository(_context);

            // Act
            var users = userRepo.Get();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<User>>(users);
            Assert.True(users.Count() > 0, $"Actual Count is: {users.Count()}");
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

            var userRepo = new UserRepository(_context);

            // Act
            await userRepo.SaveAsync(user);

            // Assert
            Assert.True(user.UserId > 0);
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

            var userRepo = new UserRepository(_context);

            // Act
            user.UserName = newUserName;

            await userRepo.SaveAsync(user);

            // Assert
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

            var userRepo = new UserRepository(_context);

            // Act
            var exception = await Record.ExceptionAsync(() => userRepo.SaveAsync(user));

            // Assert
            Assert.IsType<ArgumentNullException>(exception);
        }
    }
}