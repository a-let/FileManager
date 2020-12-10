using FileManager.Interfaces;
using FileManager.Models;

using System.Collections.Generic;

namespace FileManager.Web.Services.Interfaces
{
    public interface IEpisodeControllerService : IService<Episode>
    {
        IEnumerable<Episode> GetEpisodesBySeasonId(int seasonId);
    }
}