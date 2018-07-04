using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FileManager.Models;
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

        // GET: api/Movie/id/5
        [HttpGet("id/{id}")]
        public Movie Get(int id)
        {
            var movie = _movieControllerService.GetMovieById(id);
            return movie;
        }

        // GET: api/Movie/name/Name
        [HttpGet("name/{name}")]
        public Movie Get(string name)
        {
            var movie = _movieControllerService.GetMovieByName(name);
            return movie;
        }
        
        // POST: api/Movie
        [HttpPost]
        public bool Post([FromBody]Movie movie)
        {
            var success = _movieControllerService.SaveMovie(movie);
            return success;
        }

        // GET: api/Movie/seriesId/5
        [HttpGet("seriesId/seriesId")]
        public IEnumerable<Movie> GetBySeriesId(int seriesId)
        {
            var movies = _movieControllerService.GetMoviesBySeriesId(seriesId);
            return movies;
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