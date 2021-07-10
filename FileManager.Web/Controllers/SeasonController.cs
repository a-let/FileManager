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
    [Route("api/Season")]
    public class SeasonController : ControllerBase
    {
        private readonly IControllerService<Season> _seasonControllerService;

        public SeasonController(IControllerService<Season> seasonControllerService)
        {
            _seasonControllerService = seasonControllerService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Season>> Get()
        {
            var seasons = _seasonControllerService.Get();
            return Ok(seasons);
        }

        [HttpGet("id/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Season>> GetById(int id)
        {
            var season = await _seasonControllerService.GetByIdAsync(id);

            if (season == null)
                return NotFound();

            return Ok(season);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Season>> Post(Season season)
        {
            await _seasonControllerService.SaveAsync(season);

            return CreatedAtAction(nameof(GetById), new { Id = season.SeasonId }, season);
        }
    }
}