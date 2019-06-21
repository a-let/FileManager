using FileManager.Models;
using FileManager.Web.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Series")]
    public class SeriesController : Controller
    {
        private readonly ISeriesControllerService _seriesControllerService;
        private readonly ILog _logger;

        public SeriesController(ISeriesControllerService seriesControllerService, ILog logger)
        {
            _seriesControllerService = seriesControllerService;
            _logger = logger;
        }

        // GET: api/Series
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Series>>> Get()
        {
            try
            {
                var series = _seriesControllerService.GetSeries();
                return Ok(series);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, ex.Message);
                return BadRequest(ex);
            }
        }

        // GET: api/Series/5
        [HttpGet("id/{id}")]
        public async Task<ActionResult<Series>> GetById(int id)
        {
            try
            {
                var series = await _seriesControllerService.GetSeriesByIdAsync(id);
                return Ok(series);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, ex.Message);
                return BadRequest(ex);
            }
        }

        // GET: api/Series/name/Name
        [HttpGet("name/{name}")]
        public async Task<ActionResult<Series>> GetByName(string name)
        {
            try
            {
                var series = _seriesControllerService.GetSeriesByName(name);
                return Ok(series);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, ex.Message);
                return BadRequest(ex);
            }
        }
        
        // POST: api/Series
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody]Series series)
        {
            try
            {
                var seriesId = await _seriesControllerService.SaveSeriesAsync(series);
                return Ok(seriesId);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, ex.Message);
                return BadRequest(ex);
            }
        }
        
        // PUT: api/Series/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody]string value)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, ex.Message);
                return BadRequest(ex);
            }
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, ex.Message);
                return BadRequest(ex);
            }
        }
    }
}
