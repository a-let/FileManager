using System.Collections.Generic;
using FileManager.Models;

namespace FileManager.Services.Interfaces
{
    public interface ISeriesService
    {
        Series GetSeriesById(int id);
        Series GetSeriesByName(string name);
        IEnumerable<Series> GetSeries();
        bool SaveSeries(Series series);
    }
}