using FileManager.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Services.Interfaces
{
    public interface IEpisodeService
    {
        Task<IEnumerable<Episode>> GetEpisodes();
        Task<Episode> GetEpisodeById(int id);
        Task<Episode> GetEpisodeByName(string name);
        Task<int> SaveEpisode(Episode episode);
        Task<IEnumerable<Episode>> GetEpisodesBySeasonId(int seasonId);
    }
}