using FileManager.Models;
using FileManager.Web.Services.Interfaces;

using Microsoft.AspNetCore.Http;
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
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult<IEnumerable<Series>>> Get()
        {
            var series = await _seriesControllerService.GetAsync();
            return Ok(series);
        }

        [HttpGet("id/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Series>> GetById(int id)
        {
            var series = await _seriesControllerService.GetAsync(id);

            if (series == null)
                return NotFound();

            return Ok(series);
        }

        [HttpGet("name/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Series>> GetByName(string name)
        {
            var series = await _seriesControllerService.GetAsync(name);

            if (series == null)
                return NotFound();

            return Ok(series);
        }

        [HttpPost]
        public async Task<ActionResult<Series>> Post([FromBody]Series series)
        {
            _ = await _seriesControllerService.SaveAsync(series);

            return CreatedAtAction(nameof(GetById), new { Id = series.SeriesId }, series);
        }
    }
}