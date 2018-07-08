using System.Collections.Generic;
using FileManager.Models;

namespace FileManager.Services.Interfaces
{
    public interface ISeasonService
    {
        Season GetSeasonById(int id);
        IEnumerable<Season> GetSeasons();
        IEnumerable<Season> GetSeasonsByShowId(int showId);
        bool SaveSeason(Season season);
    }
}