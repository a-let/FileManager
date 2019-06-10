using FileManager.Models;
using FileManager.Web.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;

namespace FileManager.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Show")]
    public class ShowController : Controller
    {
        private readonly IShowControllerService _showControllerService;
        private readonly ILogger _logger;

        public ShowController(IShowControllerService showControllerService, ILogger logger)
        {
            _showControllerService = showControllerService;
            _logger = logger;
        }

        // GET: api/Show
        [HttpGet]
        public ActionResult<IEnumerable<Show>> Get()
        {
            try
            {
                var shows = _showControllerService.GetShows();
                return Ok(shows);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex);
            }
        }

        // GET: api/Show/5
        [HttpGet("id/{id}")]
        public ActionResult<Show> GetById(int id)
        {
            try
            {
                var show = _showControllerService.GetShowById(id);
                return Ok(show);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex);
            }
        }

        // GET: api/Show/name/Name
        [HttpGet("name/{name}")]
        public ActionResult<Show> GetByName(string name)
        {
            try
            {
                var show = _showControllerService.GetShowByName(name);
                return Ok(show);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex);
            }
        }
        
        // POST: api/Show
        [HttpPost]
        public ActionResult<int> Post([FromBody]Show show)
        {
            try
            {
                var showId = _showControllerService.SaveShow(show);
                return Ok(showId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex);
            }
        }
        
        // PUT: api/Show/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]string value)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex);
            }
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex);
            }
        }
    }
}