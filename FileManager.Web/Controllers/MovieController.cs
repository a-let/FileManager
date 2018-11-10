using FileManager.Models;
using FileManager.Web.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileManager.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Movie")]
    public class MovieController : Controller
    {
        private readonly IMovieControllerService _movieControllerService;
        private readonly ILogger _logger;

        public MovieController(IMovieControllerService movieControllerService, ILogger logger)
        {
            _movieControllerService = movieControllerService;
            _logger = logger;
        }

        // GET: api/Movie
        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            try
            {
                var movies = _movieControllerService.GetMovies();
                return movies;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        // GET: api/Movie/id/5
        [HttpGet("id/{id}")]
        public Movie Get(int id)
        {
            try
            {
                var movie = _movieControllerService.GetMovieById(id);
                return movie;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        // GET: api/Movie/name/Name
        [HttpGet("name/{name}")]
        public Movie Get(string name)
        {
            try
            {
                var movie = _movieControllerService.GetMovieByName(name);
                return movie;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        
        // POST: api/Movie
        [HttpPost]
        public int Post([FromBody]Movie movie)
        {
            try
            {
                var movieId = _movieControllerService.SaveMovie(movie);
                return movieId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        // GET: api/Movie/seriesId/5
        [HttpGet("seriesId/{seriesId}")]
        public IQueryable<Movie> GetBySeriesId(int seriesId)
        {
            try
            {
                var movies = _movieControllerService.GetMoviesBySeriesId(seriesId);
                return movies;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        
        // PUT: api/Movie/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}