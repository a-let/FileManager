using System.Collections.Generic;
using System.Threading.Tasks;
using FileManager.BusinessLayer;

namespace FileManager.Services.Interfaces
{
    public interface IEpisodeService
    {
        Task<IEnumerable<Episode>> GetEpisodesAsync();
        Task<Episode> GetEpisodeByIdAsync(int id);
        Task<Episode> GetEpisodeByNameAsync(string name);
        Task<bool> SaveEpisodeAsync(Episode episode);
        Task<IEnumerable<Episode>> GetEpisodeBySeasonIdAsync(int seasonId);
    }
}