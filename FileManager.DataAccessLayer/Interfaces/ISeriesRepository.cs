using FileManager.Models;
using System.Collections.Generic;

namespace FileManager.DataAccessLayer.Interfaces
{
    public interface ISeriesRepository
    {
        Series GetSeriesById(int id);
        IEnumerable<Series> GetSeries();
        Series GetSeriesByName(string name);
        bool SaveSeries(Series series);
    }
}
