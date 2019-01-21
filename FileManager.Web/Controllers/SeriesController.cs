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
        public IEnumerable<Series> Get()
        {
            try
            {
                var series = _seriesControllerService.GetSeries();
                return series;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        // GET: api/Series/5
        [HttpGet("id/{id}")]
        public Series GetById(int id)
        {
            try
            {
                var series = _seriesControllerService.GetSeriesById(id);
                return series;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        // GET: api/Series/name/Name
        [HttpGet("name/{name}")]
        public Series GetByName(string name)
        {
            try
            {
                var series = _seriesControllerService.GetSeriesByName(name);
                return series;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        
        // POST: api/Series
        [HttpPost]
        public int Post([FromBody]Series series)
        {
            try
            {
                var seriesId = _seriesControllerService.SaveSeries(series);
                return seriesId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        
        // PUT: api/Series/5
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
