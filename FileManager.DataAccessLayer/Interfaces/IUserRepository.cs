using FileManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.DataAccessLayer.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int id);
        User GetUserByUserName(string userName);
        IEnumerable<User> GetUsers();
        Task<int> SaveUserAsync(User user);
    }
}