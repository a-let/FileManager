using FileManager.DataAccessLayer.Interfaces;
using FileManager.Models;
using FileManager.Models.Dtos;
using FileManager.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Web.Services
{
    public class UserControllerService : IUserControllerService
    {
        private readonly IUserRepository _userRepository;

        public UserControllerService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<User> GetUsers() => _userRepository.GetUsers();

        public async Task<User> GetByIdAsync(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("Invalid UserId");

            return await _userRepository.GetUserByIdAsync(userId);
        }

        public User GetUserByUserName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentNullException(nameof(userName));

            return _userRepository.GetUserByUserName(userName);
        }

        public async Task<int> SaveUserAsync(UserDto user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var u = new User(user);

            if (!string.IsNullOrWhiteSpace(user.Password))
            {
                CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);

                u.PasswordHash = passwordHash;
                u.PasswordSalt = passwordSalt;
            }

            var userId = await _userRepository.SaveUserAsync(u);

            return userId;
        }

        public User Authenticate(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
                return null;

            var u = _userRepository.GetUserByUserName(userName);

            if (u == null)
                return null;

            if (!VerifyPasswordHash(password, u.PasswordHash, u.PasswordSalt))
                return null;

            return u;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("Password cannot be empty.");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("Password cannot be empty.");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i])
                        return false;
                }
            }

            return true;
        }
    }
}