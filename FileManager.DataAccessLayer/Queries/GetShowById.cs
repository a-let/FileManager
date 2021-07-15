using FileManager.DataAccessLayer.Interfaces;
using FileManager.Models.Dtos;

using Microsoft.EntityFrameworkCore;

using System.Linq;
using System.Threading.Tasks;

namespace FileManager.DataAccessLayer.Queries
{
    public class GetShowById : IQueryByIdAsync<QueryById, ShowDto>
    {
        private readonly FileManagerContext _context;

        public GetShowById(FileManagerContext context)
        {
            _context = context;
        }

        public async Task<ShowDto> QueryAsync(QueryById queryId)
        {
            var show = await _context.Show
                .Include(s => s.Seasons)?
                .ThenInclude(s => s.Episodes)?
                .AsSplitQuery()
                .OrderBy(s => s.ShowId)
                .SingleOrDefaultAsync(s => s.ShowId == queryId.Id);

            if (show == null)
                return null;

            // TODO : Better way. Ctors? AutoMapper?
            return new ShowDto
            {
                ShowId = show.ShowId,
                Name = show.Name,
                Category = show.Category,
                Path = show.Path,
                Seasons = show.Seasons.Select(s => new SeasonDto
                {
                    SeasonId = s.SeasonId,
                    ShowId = s.ShowId,
                    SeasonNumber = s.SeasonNumber,
                    Path = s.Path,
                    Episodes = s.Episodes.Select(e => new EpisodeDto
                    {
                        EpisodeId = e.EpisodeId,
                        SeasonId = e.SeasonId,
                        EpisodeNumber = e.EpisodeNumber,
                        Name = e.Name,
                        Format = e.Format,
                        Path = e.Path
                    })
                })
            };
        }
    }
}
