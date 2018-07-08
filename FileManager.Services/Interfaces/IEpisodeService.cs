using System.Collections.Generic;
using FileManager.Models;

namespace FileManager.Services.Interfaces
{
    public interface IEpisodeService
    {
        IEnumerable<Episode> GetEpisodes();
        Episode GetEpisodeById(int id);
        Episode GetEpisodeByName(string name);
        bool SaveEpisode(Episode episode);
        IEnumerable<Episode> GetEpisodesBySeasonId(int seasonId);
    }
}