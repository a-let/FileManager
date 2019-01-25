using FileManager.Models;
using FileManager.Models.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Web.Services.Interfaces
{
    public interface IUserControllerService
    {
        User Authenticate(string userName, string password);
        IEnumerable<User> GetUsers();
        Task<User> GetByIdAsync(int userId);
        User GetUserByUserName(string userName);
        Task<int> SaveUserAsync(UserDto user);
    }
}