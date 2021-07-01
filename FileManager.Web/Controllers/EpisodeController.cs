using FileManager.Models;
using FileManager.Web.Services.Interfaces;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Episode")]
    public class EpisodeController : Controller
    {
        private readonly IEpisodeControllerService _episodeControllerService;

        public EpisodeController(IEpisodeControllerService episodeControllerService)
        {
            _episodeControllerService = episodeControllerService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Episode>>> Get()
        {
            var episodes = await _episodeControllerService.GetAsync();
            return Ok(episodes);
        }

        [HttpGet("id/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Episode>> GetByIdAsync(int id)
        {
            var episode = await _episodeControllerService.GetAsync(id);

            if (episode == null)
                return NotFound();

            return Ok(episode);
        }

        [HttpGet("name/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Episode>> GetByName(string name)
        {
            var episode = await _episodeControllerService.GetAsync(name);

            if (episode == null)
                return NotFound();

            return Ok(episode);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> PostAsync([FromBody]Episode episode)
        {
            _ = await _episodeControllerService.SaveAsync(episode);

            return CreatedAtAction(nameof(GetByIdAsync), new { Id = episode.EpisodeId }, episode);
        }

        // TODO: Get episodes by season id via season controller
        [HttpGet("seasonid/{seasonId}")]
        public ActionResult<IEnumerable<Episode>> GetBySeasonId(int seasonId)
        {
            var episodes = _episodeControllerService.GetEpisodesBySeasonId(seasonId);
            return Ok(episodes);
        }
    }
}