using FileManager.Interfaces;
using FileManager.Models.Dtos;

namespace FileManager.Web.Services.Interfaces
{
    public interface IUserControllerService : IService<UserDto>
    {
        UserDto Authenticate(string userName, string password);
    }
}