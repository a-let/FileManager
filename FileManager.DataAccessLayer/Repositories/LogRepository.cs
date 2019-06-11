using FileManager.DataAccessLayer.Interfaces;
using FileManager.Models;

using System.Threading.Tasks;

namespace FileManager.DataAccessLayer.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly FileManagerContext _context;

        public LogRepository(FileManagerContext context)
        {
            _context = context;
        }

        public async Task SaveLogAsync(Log log)
        {
            if (log.LogId == 0)
                await _context.Log.AddAsync(log);
            else
            {
                var l = await _context.Log.FindAsync(log.LogId);
                _context.Entry(l).CurrentValues.SetValues(log);
            }

            await _context.SaveChangesAsync();
        }
    }
}