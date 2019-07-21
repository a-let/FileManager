using FileManager.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Services.Interfaces
{
    public interface ISeasonService : IService<Season>
    {
        Task<IEnumerable<Season>> GetSeasonsByShowId(int showId);
        Task<int> SaveAsync(Season season);
    }
}