using FileManager.Models;
using FileManager.Web.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
        private readonly ILogger _logger;

        public EpisodeController(IEpisodeControllerService episodeControllerService, ILogger logger)
        {
            _episodeControllerService = episodeControllerService;
            _logger = logger;
        }

        // GET: api/Episode
        [HttpGet]
        public ActionResult<IEnumerable<Episode>> Get()
        {
            try
            {
                var episodes = _episodeControllerService.GetEpisodes();
                return Ok(episodes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex);
            }            
        }

        // GET: api/Episode/id/5
        [HttpGet("id/{id}")]
        public async Task<ActionResult<Episode>> GetByIdAsync(int id)
        {
            try
            {
                var episode = await _episodeControllerService.GetEpisodeByIdAsync(id);
                return Ok(episode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex);
            }            
        }

        // GET: api/Episode/name/Name
        [HttpGet("name/{name}")]
        public ActionResult<Episode> GetByName(string name)
        {
            try
            {
                var episode = _episodeControllerService.GetEpisodeByName(name);
                return Ok(episode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex);
            }            
        }
        
        // POST: api/Episode
        [HttpPost]
        public async Task<ActionResult<int>> PostAsync([FromBody]Episode episode)
        {
            try
            {
                var episodeId = await _episodeControllerService.SaveEpisodeAsync(episode);
                return Ok(episodeId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex);
            }            
        }
        
        // GET: api/Episode/seasonid/5
        [HttpGet("seasonid/{seasonId}")]
        public ActionResult<IEnumerable<Episode>> GetBySeasonId(int seasonId)
        {
            try
            {
                var episodes = _episodeControllerService.GetEpisodesBySeasonId(seasonId);
                return Ok(episodes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex);
            }            
        }

        // PUT: api/Episode/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]string value)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex);
            }            
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex);
            }
        }
    }
}