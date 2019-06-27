using FileManager.Models;

using System.Threading.Tasks;

namespace FileManager.DataAccessLayer.Interfaces
{
    public interface ILogRepository
    {
        Task SaveLogAsync(Log log);
    }
}