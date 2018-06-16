using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FileManager.BusinessLayer;
using FileManager.Web.Services;

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
        public IEnumerable<Movie> Get()
        {
            var movies = _movieControllerService.GetMovies();
            return movies;
        }

        // GET: api/Movie/5
        [HttpGet("id/{id}")]
        public string Get(int id)
        {
            throw new NotImplementedException();
        }
        
        // POST: api/Movie
        [HttpPost]
        public bool Post([FromBody]Movie movie)
        {
            var success = _movieControllerService.SaveMovie(movie);
            return success;
        }
        
        // PUT: api/Movie/5
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