using FileManager.Models;
using FileManager.Web.Services.Interfaces;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Web.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/Show")]
    public class ShowController : Controller
    {
        private readonly IControllerService<Show> _showControllerService;

        public ShowController(IControllerService<Show> showControllerService)
        {
            _showControllerService = showControllerService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Show>> Get()
        {
            var shows = _showControllerService.Get();
            return Ok(shows);
        }

        [HttpGet("id/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Show>> GetById(int id)
        {
            var show = await _showControllerService.GetByIdAsync(id);

            if (show == null)
                return NotFound();

            return Ok(show);
        }

        [HttpGet("name/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Show> GetByName(string name)
        {
            var show = _showControllerService.GetByName(name);

            if (show == null)
                return NotFound();

            return Ok(show);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Show>> Post(Show show)
        {
            await _showControllerService.SaveAsync(show);

            return CreatedAtAction(nameof(GetById), new { Id = show.ShowId }, show);
        }
    }
}