using FileManager.Models;
using FileManager.Web.Services.Interfaces;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Web.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/Series")]
    public class SeriesController : Controller
    {
        private readonly IControllerService<Series> _seriesControllerService;

        public SeriesController(IControllerService<Series> seriesControllerService)
        {
            _seriesControllerService = seriesControllerService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public ActionResult<IEnumerable<Series>> Get()
        {
            var series = _seriesControllerService.Get();
            return Ok(series);
        }

        [HttpGet("id/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Series>> GetById(int id)
        {
            var series = await _seriesControllerService.GetByIdAsync(id);

            if (series == null)
                return NotFound();

            return Ok(series);
        }

        [HttpGet("name/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Series> GetByName(string name)
        {
            var series = _seriesControllerService.GetByName(name);

            if (series == null)
                return NotFound();

            return Ok(series);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Series>> Post(Series series)
        {
            await _seriesControllerService.SaveAsync(series);

            return CreatedAtAction(nameof(GetById), new { Id = series.SeriesId }, series);
        }
    }
}