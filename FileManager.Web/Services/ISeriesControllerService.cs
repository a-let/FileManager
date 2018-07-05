using System.Collections.Generic;
using FileManager.Models;

namespace FileManager.Web.Services
{
    public interface ISeriesControllerService
    {
        Series GetSeriesById(int id);
        IEnumerable<Series> GetSeries();
        Series GetSeriesByName(string name);
        bool SaveSeries(Series series);
    }
}