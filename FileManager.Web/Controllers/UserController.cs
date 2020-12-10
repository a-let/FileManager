using FileManager.Models.Dtos;
using FileManager.Web.Services.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        private readonly ITokenGenerator _tokenService;

        public UserController(IUserControllerService userControllerService, ITokenGenerator tokenService)
        {
            _userControllerService = userControllerService;
            _tokenService = tokenService;
        }

        // GET: api/User
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDto>))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<UserDto>>> Get()
        {
            var users = await _userControllerService.GetAsync();
            return Ok(users);
        }

        // GET: api/User/id/5
        [HttpGet("id/{id}")]
        [ProducesResponseType(200, Type = typeof(UserDto))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<UserDto>> GetById(int id)
        {
            var user = await _userControllerService.GetAsync(id);
            return Ok(user);
        }

        // GET: api/User/name/Name
        [HttpGet("userName/{userName}")]
        [ProducesResponseType(200, Type = typeof(UserDto))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<UserDto>> GetByUserName(string userName)
        {
            var user = await _userControllerService.GetAsync(userName);
            return Ok(user);
        }

        // POST: api/User
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<int>> Post([FromBody]UserDto user)
        {
            var userId = await _userControllerService.SaveAsync(user);
            return Ok(userId);
        }

        // POST: api/User/Authenticate
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Authenticate([FromBody]UserDto user)
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
    }
}