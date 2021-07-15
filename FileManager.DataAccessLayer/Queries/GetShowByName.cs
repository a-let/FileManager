using FileManager.DataAccessLayer.Interfaces;
using FileManager.Models.Dtos;

using Microsoft.EntityFrameworkCore;

using System.Linq;

namespace FileManager.DataAccessLayer.Queries
{
    public class GetShowByName : IQueryByName<QueryByName, ShowDto>
    {
        private readonly FileManagerContext _context;

        public GetShowByName(FileManagerContext context)
        {
            _context = context;
        }

        public ShowDto Query(QueryByName queryName)
        {
            var show = _context.Show
                .Include(s => s.Seasons)?
                .ThenInclude(s => s.Episodes)?
                .AsSplitQuery()
                .OrderBy(s => s.ShowId)
                .FirstOrDefault(s => s.Name == queryName.Name);

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