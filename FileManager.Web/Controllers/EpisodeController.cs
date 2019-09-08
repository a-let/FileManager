using FileManager.Models;
using FileManager.Web.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

using System;
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

        // GET: api/Episode
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Episode>>> Get()
        {
            var episodes = _episodeControllerService.GetEpisodes();
            return Ok(episodes);
        }

        // GET: api/Episode/id/5
        [HttpGet("id/{id}")]
        public async Task<ActionResult<Episode>> GetByIdAsync(int id)
        {
            var episode = await _episodeControllerService.GetEpisodeByIdAsync(id);
            return Ok(episode);
        }

        // GET: api/Episode/name/Name
        [HttpGet("name/{name}")]
        public async Task<ActionResult<Episode>> GetByName(string name)
        {
            var episode = _episodeControllerService.GetEpisodeByName(name);
            return Ok(episode);
        }
        
        // POST: api/Episode
        [HttpPost]
        public async Task<ActionResult<int>> PostAsync([FromBody]Episode episode)
        {
            var episodeId = await _episodeControllerService.SaveEpisodeAsync(episode);
            return Ok(episodeId);
        }
        
        // GET: api/Episode/seasonid/5
        [HttpGet("seasonid/{seasonId}")]
        public async Task<ActionResult<IEnumerable<Episode>>> GetBySeasonId(int seasonId)
        {
            var episodes = _episodeControllerService.GetEpisodesBySeasonId(seasonId);
            return Ok(episodes);
        }

        // PUT: api/Episode/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody]string value)
        {
            throw new NotImplementedException();
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}