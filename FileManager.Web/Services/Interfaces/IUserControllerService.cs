using FileManager.Models.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Web.Services.Interfaces
{
    public interface IUserControllerService
    {
        UserDto Authenticate(string userName, string password);
        IEnumerable<UserDto> GetUsers();
        Task<UserDto> GetByIdAsync(int userId);
        UserDto GetUserByUserName(string userName);
        Task<int> SaveUserAsync(UserDto user);
    }
}