using FileManager.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Web.Services.Interfaces
{
    public interface ISeasonControllerService
    {
        Task<Season> GetSeasonByIdAsync(int id);
        IEnumerable<Season> GetSeasons();
        IEnumerable<Season> GetSeasonsByShowId(int showId);
        Task<int> SaveSeasonAsync(Season season);
    }
}