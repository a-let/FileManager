using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FileManager.Models;
using FileManager.Web.Services;

namespace FileManager.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Series")]
    public class SeriesController : Controller
    {
        private readonly ISeriesControllerService _seriesControllerService;

        public SeriesController(ISeriesControllerService seriesControllerService)
        {
            _seriesControllerService = seriesControllerService;
        }

        // GET: api/Series
        [HttpGet]
        public IEnumerable<Series> Get()
        {
            var series = _seriesControllerService.GetSeries();
            return series;
        }

        // GET: api/Series/5
        [HttpGet("id/{id}")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Series
        [HttpPost]
        public bool Post([FromBody]Series series)
        {
            var success = _seriesControllerService.SaveSeries(series);
            return success;
        }
        
        // PUT: api/Series/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
