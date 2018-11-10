using FileManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileManager.DataAccessLayer.Interfaces
{
    public interface IEpisodeRepository : IDisposable
    {
        Episode GetEpisodeById(int id);
        IEnumerable<Episode> GetEpisodes();
        Episode GetEpisodeByName(string name);
        int SaveEpisode(Episode episode);
        IQueryable<Episode> GetEpisodesBySeasonId(int seasonId);
    }
}