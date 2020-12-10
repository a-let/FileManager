using FileManager.Models;
using FileManager.Web.Services.Interfaces;

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
        public async Task<ActionResult<IEnumerable<Episode>>> Get()
        {
            var episodes = await _episodeControllerService.GetAsync();
            return Ok(episodes);
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<Episode>> GetByIdAsync(int id)
        {
            var episode = await _episodeControllerService.GetAsync(id);
            return Ok(episode);
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<Episode>> GetByName(string name)
        {
            var episode = await _episodeControllerService.GetAsync(name);
            return Ok(episode);
        }

        [HttpPost]
        public async Task<ActionResult<int>> PostAsync([FromBody]Episode episode)
        {
            var episodeId = await _episodeControllerService.SaveAsync(episode);
            return Ok(episodeId);
        }
        
        [HttpGet("seasonid/{seasonId}")]
        public ActionResult<IEnumerable<Episode>> GetBySeasonId(int seasonId)
        {
            var episodes = _episodeControllerService.GetEpisodesBySeasonId(seasonId);
            return Ok(episodes);
        }
    }
}