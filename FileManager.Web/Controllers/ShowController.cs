using System;
using System.Collections.Generic;
using FileManager.Models;
using FileManager.Web.Services;
using Microsoft.AspNetCore.Mvc;

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
        public IEnumerable<Show> Get()
        {
            var shows = _showControllerService.GetShows();
            return shows;
        }

        // GET: api/Show/5
        [HttpGet("id/{id}")]
        public Show Get(int id)
        {
            var show = _showControllerService.GetShowById(id);
            return show;
        }

        // GET: api/Show/name/Name
        [HttpGet("name/{name}")]
        public Show Get(string name)
        {
            var show = _showControllerService.GetShowByName(name);
            return show;
        }
        
        // POST: api/Show
        [HttpPost]
        public bool Post([FromBody]Show show)
        {
            var success = _showControllerService.SaveShow(show);
            return success;
        }
        
        // PUT: api/Show/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            throw new NotImplementedException();
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}