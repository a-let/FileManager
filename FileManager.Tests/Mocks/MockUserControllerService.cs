using FileManager.Models.Dtos;
using FileManager.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Tests.Mocks
{
    public class MockUserControllerService : IUserControllerService
    {
        public UserDto Authenticate(string userName, string password) => new UserDto();

        public async Task<UserDto> GetByIdAsync(int userId) => await Task.FromResult(new UserDto());

        public UserDto GetUserByUserName(string userName) => new UserDto();

        public IEnumerable<UserDto> GetUsers() => new[] { new UserDto() };

        public async Task<int> SaveUserAsync(UserDto user) => await Task.FromResult(1);
    }
}