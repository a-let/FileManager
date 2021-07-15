using FileManager.DataAccessLayer.Interfaces;
using FileManager.Models;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.DataAccessLayer.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly FileManagerContext _context;

        public UserRepository(FileManagerContext context)
        {
            _context = context;
        }

        public async Task<User> GetByIdAsync(int id) =>
            await _context.User.FindAsync(id);

        public User GetByName(string userName) =>
            _context.User.FirstOrDefault(u => u.UserName == userName);

        public IEnumerable<User> Get() =>
            _context.User.ToArray();

        public async Task SaveAsync(User user)
        {
            if (user.UserId == 0)
                await _context.User.AddAsync(user);
            else
            {
                var u = await _context.User.FindAsync(user.UserId);
                _context.Entry(u).CurrentValues.SetValues(user);
            }

            await _context.SaveChangesAsync();
        }

        public Task<User> FindAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}