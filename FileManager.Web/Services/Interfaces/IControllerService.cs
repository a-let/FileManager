using FileManager.Models.Dtos;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Web.Services.Interfaces
{
    public interface IControllerService<T>
    {
        Task<T> GetByIdAsync(int id);
        IEnumerable<T> Get();
        T GetByName(string name);
        Task SaveAsync(T target);
    }

    public interface IAuthenticate
    {
        UserDto Authenticate(string userName, string password);
    }
}
