using System.Collections.Generic;
using FileManager.BusinessLayer;

namespace FileManager.Web.Services
{
    public interface ISeasonControllerService
    {
        IEnumerable<Season> GetSeasons();
        bool SaveSeason(Season season);
    }
}