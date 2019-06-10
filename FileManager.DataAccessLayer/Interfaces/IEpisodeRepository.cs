using FileManager.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.DataAccessLayer.Interfaces
{
    public interface IEpisodeRepository
    {
        Task<Episode> GetEpisodeByIdAsync(int id);
        IEnumerable<Episode> GetEpisodes();
        Episode GetEpisodeByName(string name);
        Task<int> SaveEpisodeAsync(Episode episode);
        IEnumerable<Episode> GetEpisodesBySeasonId(int seasonId);
    }
}