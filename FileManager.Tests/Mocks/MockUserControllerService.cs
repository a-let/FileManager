using FileManager.Models.Dtos;
using FileManager.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Tests.Mocks
{
    public class MockUserControllerService : IUserControllerService
    {
        public UserDto Authenticate(string userName, string password) => new UserDto();

        public async Task<UserDto> GetAsync(int userId) => await Task.FromResult(new UserDto());

        public async Task<UserDto> GetAsync(string userName) => await Task.FromResult(new UserDto());

        public async Task<IEnumerable<UserDto>> GetAsync() => await Task.FromResult(new[] { new UserDto() });

        public async Task<int> SaveAsync(UserDto user) => await Task.FromResult(1);
    }
}