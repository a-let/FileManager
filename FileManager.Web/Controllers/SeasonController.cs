using FileManager.Models;
using FileManager.Web.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;

namespace FileManager.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Season")]
    public class SeasonController : Controller
    {
        private readonly ISeasonControllerService _seasonControllerService;

        public SeasonController(ISeasonControllerService seasonControllerService)
        {
            _seasonControllerService = seasonControllerService;
        }

        // GET: api/Season
        [HttpGet]
        public IEnumerable<Season> Get()
        {
            var seasons = _seasonControllerService.GetSeasons();
            return seasons;
        }

        // GET: api/Season/5
        [HttpGet("id/{id}")]
        public Season Get(int id)
        {
            var season = _seasonControllerService.GetSeasonById(id);
            return season;
        }
        
        // POST: api/Season
        [HttpPost]
        public bool Post([FromBody]Season season)
        {
            var success = _seasonControllerService.SaveSeason(season);
            return success;
        }

        // Get: api/Season/showId/5
        [HttpGet("showId/{showId}")]
        public IEnumerable<Season> GetByShowId(int showId)
        {
            var seasons = _seasonControllerService.GetSeasonsByShowId(showId);
            return seasons;
        }
        
        // PUT: api/Season/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            throw new System.NotImplementedException();
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
