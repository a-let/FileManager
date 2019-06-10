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
        public ActionResult<IEnumerable<Movie>> Get()
        {
            try
            {
                var movies = _movieControllerService.GetMovies();
                return Ok(movies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex);
            }
        }

        // GET: api/Movie/id/5
        [HttpGet("id/{id}")]
        public ActionResult<Movie> GetById(int id)
        {
            try
            {
                var movie = _movieControllerService.GetMovieById(id);
                return Ok(movie);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        // GET: api/Movie/name/Name
        [HttpGet("name/{name}")]
        public ActionResult<Movie> GetByName(string name)
        {
            try
            {
                var movie = _movieControllerService.GetMovieByName(name);
                return Ok(movie);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex);
            }
        }
        
        // POST: api/Movie
        [HttpPost]
        public ActionResult<int> Post([FromBody]Movie movie)
        {
            try
            {
                var movieId = _movieControllerService.SaveMovie(movie);
                return Ok(movieId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex);
            }
        }

        // GET: api/Movie/seriesId/5
        [HttpGet("seriesId/{seriesId}")]
        public ActionResult<IQueryable<Movie>> GetBySeriesId(int seriesId)
        {
            try
            {
                var movies = _movieControllerService.GetMoviesBySeriesId(seriesId);
                return Ok(movies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex);
            }
        }
        
        // PUT: api/Movie/5
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