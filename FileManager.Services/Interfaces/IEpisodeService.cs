using System.Collections.Generic;
using System.Threading.Tasks;
using FileManager.BusinessLayer;

namespace FileManager.Services.Interfaces
{
    public interface IEpisodeService
    {
        Task<IEnumerable<Episode>> GetEpisodesAsync();
    }
}