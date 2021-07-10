using FileManager.Models;
using FileManager.Web.Services.Interfaces;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Web.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/Movie")]
    public class MovieController : ControllerBase
    {
        private readonly IControllerService<Movie> _movieControllerService;

        public MovieController(IControllerService<Movie> movieControllerService)
        {
            _movieControllerService = movieControllerService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Movie>> Get()
        {
            var movies = _movieControllerService.Get();
            return Ok(movies);
        }

        [HttpGet("id/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Movie>> GetById(int id)
        {
            var movie = await _movieControllerService.GetByIdAsync(id);

            if (movie == null)
                return NotFound();

            return Ok(movie);
        }

        [HttpGet("name/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Movie> GetByName(string name)
        {
            var movie = _movieControllerService.GetByName(name);

            if (movie == null)
                return NotFound();

            return Ok(movie);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Movie>> Post(Movie movie)
        {
            await _movieControllerService.SaveAsync(movie);

            return CreatedAtAction(nameof(GetById), new { Id = movie.MovieId }, movie);
        }
    }
}