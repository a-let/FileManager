using System.Collections.Generic;
using System.Linq;
using FileManager.Models;

namespace FileManager.Web.Services.Interfaces
{
    public interface IEpisodeControllerService
    {
        Episode GetEpisodeById(int id);
        IEnumerable<Episode> GetEpisodes();
        Episode GetEpisodeByName(string name);
        bool SaveEpisode(Episode episode);
        IQueryable<Episode> GetEpisodesBySeasonId(int seasonId);
    }
}