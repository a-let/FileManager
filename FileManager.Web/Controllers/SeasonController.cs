using FileManager.Models;
using FileManager.Web.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;

namespace FileManager.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Season")]
    public class SeasonController : Controller
    {
        private readonly ISeasonControllerService _seasonControllerService;
        private readonly ILogger _logger;

        public SeasonController(ISeasonControllerService seasonControllerService, ILogger logger)
        {
            _seasonControllerService = seasonControllerService;
            _logger = logger;
        }

        // GET: api/Season
        [HttpGet]
        public ActionResult<IEnumerable<Season>> Get()
        {
            try
            {
                var seasons = _seasonControllerService.GetSeasons();
                return Ok(seasons);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex);
            }
        }

        // GET: api/Season/5
        [HttpGet("id/{id}")]
        public ActionResult<Season> GetById(int id)
        {
            try
            {
                var season = _seasonControllerService.GetSeasonById(id);
                return Ok(season);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex);
            }
        }
        
        // POST: api/Season
        [HttpPost]
        public ActionResult<int> Post([FromBody]Season season)
        {
            try
            {
                var seasonId = _seasonControllerService.SaveSeason(season);
                return Ok(seasonId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex);
            }
        }

        // Get: api/Season/showId/5
        [HttpGet("showId/{showId}")]
        public ActionResult<IEnumerable<Season>> GetByShowId(int showId)
        {
            try
            {
                var seasons = _seasonControllerService.GetSeasonsByShowId(showId);
                return Ok(seasons);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex);
            }
        }
        
        // PUT: api/Season/5
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
