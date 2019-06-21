using FileManager.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.DataAccessLayer.Interfaces
{
    public interface ISeasonRepository
    {
        Task<Season> GetSeasonByIdAsync(int id);
        IEnumerable<Season> GetSeasons();
        Task<int> SaveSeasonAsync(Season season);
        IEnumerable<Season> GetSeasonsByShowId(int showId);
    }
}