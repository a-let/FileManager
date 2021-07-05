using FileManager.Models;
using FileManager.Web.Services.Interfaces;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Show")]
    public class ShowController : Controller
    {
        private readonly IShowControllerService _showControllerService;

        public ShowController(IShowControllerService showControllerService)
        {
            _showControllerService = showControllerService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Show>>> Get()
        {
            var shows = await _showControllerService.GetAsync();
            return Ok(shows);
        }

        [HttpGet("id/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Show>> GetById(int id)
        {
            var show = await _showControllerService.GetAsync(id);

            if (show == null)
                return NotFound();

            return Ok(show);
        }

        [HttpGet("name/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Show>> GetByName(string name)
        {
            var show = await _showControllerService.GetAsync(name);

            if (show == null)
                return NotFound();

            return Ok(show);
        }

        [HttpPost]
        public async Task<ActionResult<Show>> Post([FromBody]Show show)
        {
            _ = await _showControllerService.SaveAsync(show);

            return CreatedAtAction(nameof(GetById), new { Id = show.ShowId }, show);
        }
    }
}