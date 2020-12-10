using FileManager.Models;
using FileManager.Web.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Show")]
    public class ShowController : Controller
    {
        private readonly IShowControllerService _showControllerService;

        public ShowController(IShowControllerService showControllerService)
        {
            _showControllerService = showControllerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Show>>> Get()
        {
            var shows = await _showControllerService.GetAsync();
            return Ok(shows);
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<Show>> GetById(int id)
        {
            var show = await _showControllerService.GetAsync(id);
            return Ok(show);
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<Show>> GetByName(string name)
        {
            var show = await _showControllerService.GetAsync(name);
            return Ok(show);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody]Show show)
        {
            var showId = await _showControllerService.SaveAsync(show);
            return Ok(showId);
        }
    }
}