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
    public class UserControllerService : IControllerService<UserDto>, IAuthenticate
    {
        private readonly IRepository<User> _userRepository;
        private readonly ICryptographyService _cryptoService;

        public UserControllerService(IRepository<User> userRepository, ICryptographyService cryptoService)
        {
            _userRepository = userRepository;
            _cryptoService = cryptoService;
        }

        public async Task<UserDto> GetByIdAsync(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("Invalid UserId");

            return new UserDto(await _userRepository.GetByIdAsync(userId));
        }

        public IEnumerable<UserDto> Get() =>
            _userRepository.Get()
                .Select(u => new UserDto(u));

        public UserDto GetByName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentNullException(nameof(userName));

            return new UserDto(_userRepository.GetByName(userName));
        }

        public async Task SaveAsync(UserDto user)
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

            await _userRepository.SaveAsync(u);
        }

        // TODO: Serperate from user controller
        public UserDto Authenticate(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
                return null;

            var u = _userRepository.GetByName(userName);

            if (u == null)
                return null;

            if (!_cryptoService.VerifyHash(password, u.PasswordHash, u.PasswordSalt))
                return null;

            return new UserDto(u);
        }
    }
}