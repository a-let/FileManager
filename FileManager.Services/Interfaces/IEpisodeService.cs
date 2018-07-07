using System.Collections.Generic;
using System.Threading.Tasks;
using FileManager.Models;

namespace FileManager.Services.Interfaces
{
    public interface IEpisodeService
    {
        IEnumerable<Episode> GetEpisodes();
        Episode GetEpisodeById(int id);
        Episode GetEpisodeByName(string name);
        Task<bool> SaveEpisodeAsync(Episode episode);
        IEnumerable<Episode> GetEpisodesBySeasonId(int seasonId);
    }
}