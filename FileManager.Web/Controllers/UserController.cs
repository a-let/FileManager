using FileManager.Models.Dtos;
using FileManager.Web.Services.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Web.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : ControllerBase
    {
        private readonly IUserControllerService _userControllerService;
        private readonly ILog _logger;
        private readonly ITokenGenerator _tokenService;

        public UserController(IUserControllerService userControllerService,
            ILog logger, ITokenGenerator tokenService)
        {
            _userControllerService = userControllerService;
            _logger = logger;
            _tokenService = tokenService;
        }

        // GET: api/User
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDto>))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<UserDto>>> Get()
        {
            try
            {
                var users = _userControllerService.GetUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, ex.Message);
                return BadRequest(ex);
            }
        }

        // GET: api/User/id/5
        [HttpGet("id/{id}")]
        [ProducesResponseType(200, Type = typeof(UserDto))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<UserDto>> GetById(int id)
        {
            try
            {
                var user = await _userControllerService.GetByIdAsync(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, ex.Message);
                return BadRequest(ex);
            }
        }

        // GET: api/User/name/Name
        [HttpGet("userName/{userName}")]
        [ProducesResponseType(200, Type = typeof(UserDto))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<UserDto>> GetByUserName(string userName)
        {
            try
            {
                var user = _userControllerService.GetUserByUserName(userName);
                return Ok(user);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, ex.Message);
                return BadRequest(ex);
            }
        }

        // POST: api/User
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<int>> Post([FromBody]UserDto user)
        {
            try
            {
                var userId = await _userControllerService.SaveUserAsync(user);
                return Ok(userId);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, ex.Message);
                return BadRequest(ex);
            }
        }

        // POST: api/User/Authenticate
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Authenticate([FromBody]UserDto user)
        {
            try
            {
                var u = _userControllerService.Authenticate(user.UserName, user.Password);

                if (u == null)
                    return BadRequest(new { message = "Username or Password is incorrect." });

                return Ok(new
                {
                    Id = u.UserId,
                    Username = u.UserName,
                    u.FirstName,
                    u.LastName,
                    Token = _tokenService.GenerateToken(u.UserName)
                });
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, ex.Message);
                return BadRequest(ex);
            }
        }
    }
}