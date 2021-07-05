using FileManager.Models;
using FileManager.Web.Services.Interfaces;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Season")]
    public class SeasonController : ControllerBase
    {
        private readonly ISeasonControllerService _seasonControllerService;

        public SeasonController(ISeasonControllerService seasonControllerService)
        {
            _seasonControllerService = seasonControllerService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Season>>> Get()
        {
            var seasons = await _seasonControllerService.GetAsync();
            return Ok(seasons);
        }

        [HttpGet("id/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Season>> GetById(int id)
        {
            var season = await _seasonControllerService.GetAsync(id);

            if (season == null)
                return NotFound();

            return Ok(season);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Season>> Post([FromBody]Season season)
        {
            _ = await _seasonControllerService.SaveAsync(season);

            return CreatedAtAction(nameof(GetById), new { Id = season.SeasonId }, season);
        }

        // TODO: Get seasons by show id via show controller
        [HttpGet("showId/{showId}")]
        public ActionResult<IEnumerable<Season>> GetByShowId(int showId)
        {
            var seasons = _seasonControllerService.GetSeasonsByShowId(showId);
            return Ok(seasons);
        }
    }
}