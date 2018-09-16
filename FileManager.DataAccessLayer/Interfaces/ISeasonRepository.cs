using FileManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileManager.DataAccessLayer.Interfaces
{
    public interface ISeasonRepository : IDisposable
    {
        Season GetSeasonById(int id);
        IEnumerable<Season> GetSeasons();
        bool SaveSeason(Season season);
        IQueryable<Season> GetSeasonsByShowId(int showId);
    }
}