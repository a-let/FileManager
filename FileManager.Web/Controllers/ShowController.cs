using FileManager.DataAccessLayer.Interfaces;
using FileManager.DataAccessLayer.Queries;
using FileManager.Models;
using FileManager.Models.Dtos;
using FileManager.Web.Commands;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Web.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/Show")]
    public class ShowController : Controller
    {
        private readonly IRepository<Show> _showRepository;
        private readonly IQueryByIdAsync<QueryById, ShowDto> _queryById;
        private readonly IQueryByName<QueryByName, ShowDto> _queryByName;
        private readonly IQueryForList<ShowDto> _queryForShows;

        public ShowController(IRepository<Show> showRepository,
            IQueryByIdAsync<QueryById, ShowDto> queryById,
            IQueryByName<QueryByName, ShowDto> queryByName,
            IQueryForList<ShowDto> queryForShows)
        {
            _showRepository = showRepository;
            _queryById = queryById;
            _queryByName = queryByName;
            _queryForShows = queryForShows;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ShowDto>> Get()
        {
            var shows = _queryForShows.Query();
            return Ok(shows);
        }

        [HttpGet("id/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ShowDto>> GetById(int id)
        {
            var show = await _queryById.QueryAsync(id);

            if (show == null)
                return NotFound();

            return Ok(show);
        }

        [HttpGet("name/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ShowDto> GetByName(string name)
        {
            var show = _queryByName.Query(name);

            if (show == null)
                return NotFound();

            return Ok(show);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ShowDto>> Put(ShowUpdateCommand cmd, int id)
        {
            var show = await _showRepository.FindAsync(id);

            if (show == null)
                return NotFound();

            show.Name = cmd.Name;
            show.Category = cmd.Category;

            show.UpdatePath(cmd.Path);

            await _showRepository.SaveAsync(show);

            var showDto = await _queryById.QueryAsync(id);

            return CreatedAtAction(nameof(GetById), new { Id = id }, showDto);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ShowDto>> Post(ShowCreateCommand cmd)
        {
            var show = new Show(0, cmd.Name, cmd.Category, cmd.Path);
            
            await _showRepository.SaveAsync(show);

            var showDto = await _queryById.QueryAsync(show.ShowId);

            return CreatedAtAction(nameof(GetById), new { Id = showDto.ShowId }, showDto);
        }
    }
}