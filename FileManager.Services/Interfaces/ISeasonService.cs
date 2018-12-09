using FileManager.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Services.Interfaces
{
    public interface ISeasonService
    {
        Task<Season> GetSeasonById(int id);
        Task<IEnumerable<Season>> GetSeasons();
        Task<IEnumerable<Season>> GetSeasonsByShowId(int showId);
        Task<int> SaveSeason(Season season);
    }
}