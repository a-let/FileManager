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
    [Route("api/Episode")]
    public class EpisodeController : ControllerBase
    {
        private readonly IControllerService<Episode> _episodeControllerService;

        public EpisodeController(IControllerService<Episode> episodeControllerService)
        {
            _episodeControllerService = episodeControllerService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Episode>> Get()
        {
            var episodes = _episodeControllerService.Get();
            return Ok(episodes);
        }

        [HttpGet("id/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Episode>> GetByIdAsync(int id)
        {
            var episode = await _episodeControllerService.GetByIdAsync(id);

            if (episode == null)
                return NotFound();

            return Ok(episode);
        }

        [HttpGet("name/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Episode> GetByName(string name)
        {
            var episode = _episodeControllerService.GetByName(name);

            if (episode == null)
                return NotFound();

            return Ok(episode);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Episode>> PostAsync(Episode episode)
        {
            await _episodeControllerService.SaveAsync(episode);

            return CreatedAtAction(nameof(GetByIdAsync), new { Id = episode.EpisodeId }, episode);
        }
    }
}