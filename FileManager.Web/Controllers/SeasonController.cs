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
        public IEnumerable<Season> Get()
        {
            try
            {
                var seasons = _seasonControllerService.GetSeasons();
                return seasons;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        // GET: api/Season/5
        [HttpGet("id/{id}")]
        public Season GetById(int id)
        {
            try
            {
                var season = _seasonControllerService.GetSeasonById(id);
                return season;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        
        // POST: api/Season
        [HttpPost]
        public int Post([FromBody]Season season)
        {
            try
            {
                var seasonId = _seasonControllerService.SaveSeason(season);
                return seasonId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        // Get: api/Season/showId/5
        [HttpGet("showId/{showId}")]
        public IEnumerable<Season> GetByShowId(int showId)
        {
            try
            {
                var seasons = _seasonControllerService.GetSeasonsByShowId(showId);
                return seasons;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        
        // PUT: api/Season/5
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
