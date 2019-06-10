using System.Collections.Generic;
using System.Threading.Tasks;
using FileManager.Models;

namespace FileManager.Web.Services.Interfaces
{
    public interface IEpisodeControllerService
    {
        Task<Episode> GetEpisodeByIdAsync(int id);
        IEnumerable<Episode> GetEpisodes();
        Episode GetEpisodeByName(string name);
        Task<int> SaveEpisodeAsync(Episode episode);
        IEnumerable<Episode> GetEpisodesBySeasonId(int seasonId);
    }
}