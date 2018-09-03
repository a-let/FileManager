using FileManager.Models;
using System.Collections.Generic;
using System.Linq;

namespace FileManager.DataAccessLayer.Interfaces
{
    public interface ISeasonRepository
    {
        Season GetSeasonById(int id);
        IEnumerable<Season> GetSeasons();
        bool SaveSeason(Season season);
        IQueryable<Season> GetSeasonsByShowId(int id);
    }
}