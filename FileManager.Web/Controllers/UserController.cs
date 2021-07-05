using FileManager.Models.Dtos;
using FileManager.Web.Services.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserDto>>> Get()
        {
            var users = await _userControllerService.GetAsync();
            return Ok(users);
        }

        [HttpGet("id/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> GetById(int id)
        {
            var user = await _userControllerService.GetAsync(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpGet("userName/{userName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> GetByUserName(string userName)
        {
            var user = await _userControllerService.GetAsync(userName);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDto>> Post([FromBody]UserDto user)
        {
            _ = await _userControllerService.SaveAsync(user);

            return CreatedAtAction(nameof(GetById), new { Id = user.UserId }, user);
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Authenticate([FromBody]UserDto user)
        {
            var u = _userControllerService.Authenticate(user.UserName, user.Password);

            if (u == null)
                return BadRequest(new { message = "Username or Password is incorrect." });

            return Accepted(new
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