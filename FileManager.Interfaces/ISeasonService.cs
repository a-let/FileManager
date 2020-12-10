using FileManager.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Interfaces
{
    public interface ISeasonService : IService<Season>
    {
        Task<IEnumerable<Season>> GetSeasonsByShowId(int showId);
    }
}