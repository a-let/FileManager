using FileManager.Models;
using FileManager.Web.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Series>>> Get()
        {
            var series = await _seriesControllerService.GetAsync();
            return Ok(series);
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<Series>> GetById(int id)
        {
            var series = await _seriesControllerService.GetAsync(id);
            return Ok(series);
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<Series>> GetByName(string name)
        {
            var series = await _seriesControllerService.GetAsync(name);
            return Ok(series);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody]Series series)
        {
            var seriesId = await _seriesControllerService.SaveAsync(series);
            return Ok(seriesId);
        }
    }
}