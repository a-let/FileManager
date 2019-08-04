using FileManager.Models;
using FileManager.Web.Services.Interfaces;
using Logging;

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
        private readonly ILogger _logger;

        public MovieController(IMovieControllerService movieControllerService, ILogger logger)
        {
            _movieControllerService = movieControllerService;
            _logger = logger;
        }

        // GET: api/Movie
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> Get()
        {
            try
            {
                var movies = _movieControllerService.GetMovies();
                return Ok(movies);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, ex.Message);
                return BadRequest(ex);
            }
        }

        // GET: api/Movie/id/5
        [HttpGet("id/{id}")]
        public async Task<ActionResult<Movie>> GetById(int id)
        {
            try
            {
                var movie = await _movieControllerService.GetMovieByIdAsync(id);
                return Ok(movie);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, ex.Message);
                throw;
            }
        }

        // GET: api/Movie/name/Name
        [HttpGet("name/{name}")]
        public async Task<ActionResult<Movie>> GetByName(string name)
        {
            try
            {
                var movie = _movieControllerService.GetMovieByName(name);
                return Ok(movie);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, ex.Message);
                return BadRequest(ex);
            }
        }
        
        // POST: api/Movie
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody]Movie movie)
        {
            try
            {
                var movieId = await _movieControllerService.SaveMovieAsync(movie);
                return Ok(movieId);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, ex.Message);
                return BadRequest(ex);
            }
        }

        // GET: api/Movie/seriesId/5
        [HttpGet("seriesId/{seriesId}")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetBySeriesId(int seriesId)
        {
            try
            {
                var movies = _movieControllerService.GetMoviesBySeriesId(seriesId);
                return Ok(movies);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, ex.Message);
                return BadRequest(ex);
            }
        }
        
        // PUT: api/Movie/5
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