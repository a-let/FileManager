using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManager.BusinessLayer;
using FileManager.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileManager.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Series")]
    public class SeriesController : Controller
    {
        private readonly ISeriesContollerService _seriesControllerService;

        public SeriesController(ISeriesContollerService seriesControllerService)
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
        [HttpGet("{id}", Name = "Get")]
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
