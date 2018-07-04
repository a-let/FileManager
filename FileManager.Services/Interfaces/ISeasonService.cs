using System.Collections.Generic;
using System.Threading.Tasks;
using FileManager.Models;

namespace FileManager.Services.Interfaces
{
    public interface ISeasonService
    {
        Task<Season> GetSeasonByIdAsync(int id);
        Task<IEnumerable<Season>> GetSeasonsAsync();
        Task<IEnumerable<Season>> GetSeasonsByShowIdAsync(int showId);
        Task<bool> SaveSeasonAsync(Season season);
    }
}