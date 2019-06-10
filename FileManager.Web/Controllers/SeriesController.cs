using FileManager.Models;
using FileManager.Web.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;

namespace FileManager.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Series")]
    public class SeriesController : Controller
    {
        private readonly ISeriesControllerService _seriesControllerService;
        private readonly ILogger _logger;

        public SeriesController(ISeriesControllerService seriesControllerService, ILogger logger)
        {
            _seriesControllerService = seriesControllerService;
            _logger = logger;
        }

        // GET: api/Series
        [HttpGet]
        public ActionResult<IEnumerable<Series>> Get()
        {
            try
            {
                var series = _seriesControllerService.GetSeries();
                return Ok(series);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex);
            }
        }

        // GET: api/Series/5
        [HttpGet("id/{id}")]
        public ActionResult<Series> GetById(int id)
        {
            try
            {
                var series = _seriesControllerService.GetSeriesById(id);
                return Ok(series);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex);
            }
        }

        // GET: api/Series/name/Name
        [HttpGet("name/{name}")]
        public ActionResult<Series> GetByName(string name)
        {
            try
            {
                var series = _seriesControllerService.GetSeriesByName(name);
                return Ok(series);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex);
            }
        }
        
        // POST: api/Series
        [HttpPost]
        public ActionResult<int> Post([FromBody]Series series)
        {
            try
            {
                var seriesId = _seriesControllerService.SaveSeries(series);
                return Ok(seriesId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex);
            }
        }
        
        // PUT: api/Series/5
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
