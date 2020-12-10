using FileManager.Models;
using FileManager.Web.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Season")]
    public class SeasonController : Controller
    {
        private readonly ISeasonControllerService _seasonControllerService;

        public SeasonController(ISeasonControllerService seasonControllerService)
        {
            _seasonControllerService = seasonControllerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Season>>> Get()
        {
            var seasons = await _seasonControllerService.GetAsync();
            return Ok(seasons);
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<Season>> GetById(int id)
        {
            var season = await _seasonControllerService.GetAsync(id);
            return Ok(season);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody]Season season)
        {
            var seasonId = await _seasonControllerService.SaveAsync(season);
            return Ok(seasonId);
        }

        [HttpGet("showId/{showId}")]
        public ActionResult<IEnumerable<Season>> GetByShowId(int showId)
        {
            var seasons = _seasonControllerService.GetSeasonsByShowId(showId);
            return Ok(seasons);
        }
    }
}