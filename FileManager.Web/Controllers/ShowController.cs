using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManager.BusinessLayer;
using FileManager.Web.Services;
using Microsoft.AspNetCore.Http;
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
        public string Get(int id)
        {
            return "value";
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
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
