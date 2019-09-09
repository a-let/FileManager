using FileManager.Models;
using FileManager.Web.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<ActionResult<IEnumerable<Season>>> Get()
        {
            var seasons = _seasonControllerService.GetSeasons();
            return Ok(seasons);
        }

        // GET: api/Season/5
        [HttpGet("id/{id}")]
        public async Task<ActionResult<Season>> GetById(int id)
        {
            var season = await _seasonControllerService.GetSeasonByIdAsync(id);
            return Ok(season);
        }
        
        // POST: api/Season
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody]Season season)
        {
            var seasonId = await _seasonControllerService.SaveSeasonAsync(season);
            return Ok(seasonId);
        }

        // Get: api/Season/showId/5
        [HttpGet("showId/{showId}")]
        public async Task<ActionResult<IEnumerable<Season>>> GetByShowId(int showId)
        {
            var seasons = _seasonControllerService.GetSeasonsByShowId(showId);
            return Ok(seasons);
        }
        
        // PUT: api/Season/5
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
