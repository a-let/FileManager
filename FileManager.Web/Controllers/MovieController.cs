using FileManager.Models;
using FileManager.Web.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Movie")]
    public class MovieController : Controller
    {
        private readonly IMovieControllerService _movieControllerService;

        public MovieController(IMovieControllerService movieControllerService)
        {
            _movieControllerService = movieControllerService;
        }

        // GET: api/Movie
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> Get()
        {
            var movies = await _movieControllerService.GetAsync();
            return Ok(movies);
        }

        // GET: api/Movie/id/5
        [HttpGet("id/{id}")]
        public async Task<ActionResult<Movie>> GetById(int id)
        {
            var movie = await _movieControllerService.GetAsync(id);
            return Ok(movie);
        }

        // GET: api/Movie/name/Name
        [HttpGet("name/{name}")]
        public async Task<ActionResult<Movie>> GetByName(string name)
        {
            var movie = await _movieControllerService.GetAsync(name);
            return Ok(movie);
        }
        
        // POST: api/Movie
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody]Movie movie)
        {
            var movieId = await _movieControllerService.SaveAsync(movie);
            return Ok(movieId);
        }

        // GET: api/Movie/seriesId/5
        [HttpGet("seriesId/{seriesId}")]
        public ActionResult<IEnumerable<Movie>> GetBySeriesId(int seriesId)
        {
            var movies = _movieControllerService.GetMoviesBySeriesId(seriesId);
            return Ok(movies);
        }
        
        // PUT: api/Movie/5
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