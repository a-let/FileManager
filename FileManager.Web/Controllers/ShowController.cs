using FileManager.Models;
using FileManager.Web.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

using System;
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

        // GET: api/Show
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Show>>> Get()
        {
            var shows = await _showControllerService.GetAsync();
            return Ok(shows);
        }

        // GET: api/Show/5
        [HttpGet("id/{id}")]
        public async Task<ActionResult<Show>> GetById(int id)
        {
            var show = await _showControllerService.GetAsync(id);
            return Ok(show);
        }

        // GET: api/Show/name/Name
        [HttpGet("name/{name}")]
        public async Task<ActionResult<Show>> GetByName(string name)
        {
            var show = await _showControllerService.GetAsync(name);
            return Ok(show);
        }
        
        // POST: api/Show
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody]Show show)
        {
            var showId = await _showControllerService.SaveAsync(show);
            return Ok(showId);
        }
        
        // PUT: api/Show/5
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