using FileManager.Models;
using FileManager.Models.Dtos;
using FileManager.Web.Services.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Web.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : ControllerBase
    {
        private readonly IUserControllerService _userControllerService;
        private readonly ILogger _logger;
        private readonly string _key;

        public UserController(IUserControllerService userControllerService, ILogger logger, IConfiguration config)
        {
            _userControllerService = userControllerService;
            _logger = logger;

            _key = config["Secret"];
        }

        // GET: api/User
        [HttpGet]
        public IEnumerable<User> Get()
        {
            try
            {
                var users = _userControllerService.GetUsers();
                return users;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        // GET: api/User/id/5
        [HttpGet("id/{id}")]
        public async Task<User> GetById(int id)
        {
            try
            {
                var user = await _userControllerService.GetByIdAsync(id);
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        // GET: api/User/name/Name
        [HttpGet("name/{name}")]
        public User GetByUserName(string name)
        {
            try
            {
                var user = _userControllerService.GetUserByUserName(name);
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        // POST: api/User
        [AllowAnonymous]
        [HttpPost]
        public async Task<int> Post([FromBody]UserDto user)
        {
            try
            {
                var userId = await _userControllerService.SaveUserAsync(user);
                return userId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        // POST: api/User/Authenticate
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody]UserDto user)
        {
            try
            {
                var u = _userControllerService.Authenticate(user.UserName, user.Password);

                if (u == null)
                    return BadRequest(new { message = "Username or Password is incorrect." });

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, u.UserName)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_key)), SecurityAlgorithms.HmacSha256Signature),
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(new
                {
                    Id = u.UserId,
                    Username = u.UserName,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Token = tokenString
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}