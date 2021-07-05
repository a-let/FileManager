using FileManager.Models;
using FileManager.Web.Services.Interfaces;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Movie")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieControllerService _movieControllerService;

        public MovieController(IMovieControllerService movieControllerService)
        {
            _movieControllerService = movieControllerService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Movie>>> Get()
        {
            var movies = await _movieControllerService.GetAsync();
            return Ok(movies);
        }

        [HttpGet("id/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Movie>> GetById(int id)
        {
            var movie = await _movieControllerService.GetAsync(id);

            if (movie == null)
                return NotFound();

            return Ok(movie);
        }

        [HttpGet("name/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Movie>> GetByName(string name)
        {
            var movie = await _movieControllerService.GetAsync(name);

            if (movie == null)
                return NotFound();

            return Ok(movie);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Movie>> Post([FromBody]Movie movie)
        {
            _ = await _movieControllerService.SaveAsync(movie);

            return CreatedAtAction(nameof(GetById), new { Id = movie.MovieId }, movie);
        }

        // TODO: Get movies by series id via series controller
        [HttpGet("seriesId/{seriesId}")]
        public ActionResult<IEnumerable<Movie>> GetBySeriesId(int seriesId)
        {
            var movies = _movieControllerService.GetMoviesBySeriesId(seriesId);
            return Ok(movies);
        }
    }
}