using System.Collections.Generic;
using FileManager.BusinessLayer;

namespace FileManager.Web.Services
{
    public interface IEpisodeControllerService
    {
        Episode GetEpisodeById(int id);
        IEnumerable<Episode> GetEpisodes();
        Episode GetEpisodeByName(string name);
        bool SaveEpisode(Episode episode);
        IEnumerable<Episode> GetEpisodesBySeasonId(int seasonId);
    }
}