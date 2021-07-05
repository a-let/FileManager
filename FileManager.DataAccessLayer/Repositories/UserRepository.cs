using FileManager.DataAccessLayer.Interfaces;
using FileManager.Models;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.DataAccessLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FileManagerContext _context;

        public UserRepository(FileManagerContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByIdAsync(int id) => await _context.User.FindAsync(id);

        public User GetUserByUserName(string userName) => _context.User.FirstOrDefault(u => u.UserName == userName);

        public IEnumerable<User> GetUsers() => _context.User.ToArray();

        public async Task<int> SaveUserAsync(User user)
        {
            if (user.UserId == 0)
                await _context.User.AddAsync(user);
            else
            {
                var u = await _context.User.FindAsync(user.UserId);
                _context.Entry(u).CurrentValues.SetValues(user);
            }

            await _context.SaveChangesAsync();

            return user.UserId;
        }
    }
}