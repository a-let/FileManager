using FileManager.Models;
using FileManager.Web.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;

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
        public IEnumerable<Episode> Get()
        {
            try
            {
                var episodes = _episodeControllerService.GetEpisodes();
                return episodes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }            
        }

        // GET: api/Episode/id/5
        [HttpGet("id/{id}")]
        public Episode GetById(int id)
        {
            try
            {
                var episode = _episodeControllerService.GetEpisodeById(id);
                return episode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }            
        }

        // GET: api/Episode/name/Name
        [HttpGet("name/{name}")]
        public Episode GetByName(string name)
        {
            try
            {
                var episode = _episodeControllerService.GetEpisodeByName(name);
                return episode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }            
        }
        
        // POST: api/Episode
        [HttpPost]
        public int Post([FromBody]Episode episode)
        {
            try
            {
                var episodeId = _episodeControllerService.SaveEpisode(episode);
                return episodeId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }            
        }
        
        // GET: api/Episode/seasonid/5
        [HttpGet("seasonid/{seasonId}")]
        public IEnumerable<Episode> GetBySeasonId(int seasonId)
        {
            try
            {
                var episodes = _episodeControllerService.GetEpisodesBySeasonId(seasonId);
                return episodes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }            
        }

        // PUT: api/Episode/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }            
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}