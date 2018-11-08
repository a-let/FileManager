using FileManager.Models;

namespace FileManager.DataAccessLayer.Interfaces
{
    public interface ILogRepository
    {
        bool SaveLog(Log log);
    }
}