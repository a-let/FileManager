using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.DataAccessLayer.Interfaces
{
    public interface IRepository<T>
    {
        Task<T> GetByIdAsync(int id);
        IEnumerable<T> Get();
        T GetByName(string name);
        Task SaveAsync(T target);
    }
}
