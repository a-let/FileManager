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

        public SeriesController(ISeriesControllerService seriesControllerService)
        {
            _seriesControllerService = seriesControllerService;
        }

        // GET: api/Series
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Series>>> Get()
        {
            var series = await _seriesControllerService.GetAsync();
            return Ok(series);
        }

        // GET: api/Series/5
        [HttpGet("id/{id}")]
        public async Task<ActionResult<Series>> GetById(int id)
        {
            var series = await _seriesControllerService.GetAsync(id);
            return Ok(series);
        }

        // GET: api/Series/name/Name
        [HttpGet("name/{name}")]
        public async Task<ActionResult<Series>> GetByName(string name)
        {
            var series = await _seriesControllerService.GetAsync(name);
            return Ok(series);
        }
        
        // POST: api/Series
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody]Series series)
        {
            var seriesId = await _seriesControllerService.SaveAsync(series);
            return Ok(seriesId);
        }
        
        // PUT: api/Series/5
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
