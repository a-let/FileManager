using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FileManager.BusinessLayer;
using FileManager.Web.Services;

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
        public IEnumerable<Episode> Get()
        {
            var episodes = _episodeControllerService.GetEpisodes();
            return episodes;
        }

        // GET: api/Episode/id/5
        [HttpGet("id/{id}")]
        public Episode Get(int id)
        {
            var episode = _episodeControllerService.GetEpisodeById(id);
            return episode;
        }

        // GET: api/Episode/name/Name
        [HttpGet("name/{name}")]
        public Episode Get(string name)
        {
            var episode = _episodeControllerService.GetEpisodeByName(name);
            return episode;
        }
        
        // POST: api/Episode
        [HttpPost]
        public bool Post([FromBody]Episode episode)
        {
            var success = _episodeControllerService.SaveEpisode(episode);
            return success;
        }
        
        // GET: api/Episode/seasonid/5
        [HttpGet("seasonid/{seasonId}")]
        public IEnumerable<Episode> GetBySeasonId(int seasonId)
        {
            var episodes = _episodeControllerService.GetEpisodesBySeasonId(seasonId);
            return episodes;
        }

        // PUT: api/Episode/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            throw new NotImplementedException();
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}