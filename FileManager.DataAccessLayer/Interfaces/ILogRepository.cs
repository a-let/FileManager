using FileManager.Models;

namespace FileManager.DataAccessLayer.Interfaces
{
    public interface ILogRepository
    {
        void SaveLog(Log log);
    }
}