using FileManager.DataAccessLayer.Interfaces;
using FileManager.Models;
using FileManager.Models.Dtos;
using FileManager.Web.Services.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Web.Services
{
    public class UserControllerService : IUserControllerService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICryptographyService _cryptoService;

        public UserControllerService(IUserRepository userRepository, ICryptographyService cryptoService)
        {
            _userRepository = userRepository;
            _cryptoService = cryptoService;
        }

        public IEnumerable<UserDto> GetUsers() => _userRepository.GetUsers().Select(u => new UserDto(u));

        public async Task<UserDto> GetByIdAsync(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("Invalid UserId");

            return new UserDto(await _userRepository.GetUserByIdAsync(userId));
        }

        public UserDto GetUserByUserName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentNullException(nameof(userName));

            return new UserDto(_userRepository.GetUserByUserName(userName));
        }

        public async Task<int> SaveUserAsync(UserDto user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var u = new User(user);

            if (!string.IsNullOrWhiteSpace(user.Password))
            {
                _cryptoService.CreateHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);

                u.PasswordHash = passwordHash;
                u.PasswordSalt = passwordSalt;
            }

            var userId = await _userRepository.SaveUserAsync(u);

            return userId;
        }

        public UserDto Authenticate(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
                return null;

            var u = _userRepository.GetUserByUserName(userName);

            if (u == null)
                return null;

            if (!_cryptoService.VerifyHash(password, u.PasswordHash, u.PasswordSalt))
                return null;

            return new UserDto(u);
        }
    }
}