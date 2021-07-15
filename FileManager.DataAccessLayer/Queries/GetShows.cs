using FileManager.DataAccessLayer.Interfaces;
using FileManager.Models.Dtos;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;

namespace FileManager.DataAccessLayer.Queries
{
    public class GetShows : IQueryForList<ShowDto>
    {
        private readonly FileManagerContext _context;

        public GetShows(FileManagerContext context)
        {
            _context = context;
        }

        public IEnumerable<ShowDto> Query()
        {
            var shows = _context.Show
                .Include(s => s.Seasons)?
                .ThenInclude(s => s.Episodes)?
                .AsSplitQuery()
                .ToList(); // TODO : ToListAsync()?

            if (shows == null)
                return null;

            // TODO : Better way. Ctors? AutoMapper?
            return new List<ShowDto>(shows.Select(show => new ShowDto
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
            }));
        }
    }
}