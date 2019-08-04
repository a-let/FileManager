using FileManager.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Services.Interfaces
{
    public interface IEpisodeService : IService<Episode>
    {
        Task<IEnumerable<Episode>> GetEpisodesBySeasonId(int seasonId);
    }
}