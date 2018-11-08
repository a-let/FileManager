using FileManager.DataAccessLayer.Interfaces;
using FileManager.Models;

namespace FileManager.DataAccessLayer.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly FileManagerContext _context;

        public LogRepository(FileManagerContext context)
        {
            _context = context;
        }

        public bool SaveLog(Log log)
        {
            try
            {
                if (log.LogId == 0)
                    _context.Log.Add(log);
                else
                {
                    var l = _context.Log.Find(log.LogId);
                    _context.Entry(l).CurrentValues.SetValues(log);
                }

                _context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}