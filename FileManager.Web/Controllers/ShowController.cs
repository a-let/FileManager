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
        private readonly ILog _logger;

        public ShowController(IShowControllerService showControllerService, ILog logger)
        {
            _showControllerService = showControllerService;
            _logger = logger;
        }

        // GET: api/Show
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Show>>> Get()
        {
            try
            {
                var shows = _showControllerService.GetShows();
                return Ok(shows);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, ex.Message);
                return BadRequest(ex);
            }
        }

        // GET: api/Show/5
        [HttpGet("id/{id}")]
        public async Task<ActionResult<Show>> GetById(int id)
        {
            try
            {
                var show = _showControllerService.GetShowById(id);
                return Ok(show);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, ex.Message);
                return BadRequest(ex);
            }
        }

        // GET: api/Show/name/Name
        [HttpGet("name/{name}")]
        public async Task<ActionResult<Show>> GetByName(string name)
        {
            try
            {
                var show = _showControllerService.GetShowByName(name);
                return Ok(show);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, ex.Message);
                return BadRequest(ex);
            }
        }
        
        // POST: api/Show
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody]Show show)
        {
            try
            {
                var showId = _showControllerService.SaveShow(show);
                return Ok(showId);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, ex.Message);
                return BadRequest(ex);
            }
        }
        
        // PUT: api/Show/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody]string value)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, ex.Message);
                return BadRequest(ex);
            }
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, ex.Message);
                return BadRequest(ex);
            }
        }
    }
}