using System.Collections.Generic;
using FileManager.Models;

namespace FileManager.Web.Services.Interfaces
{
    public interface ISeriesControllerService
    {
        Series GetSeriesById(int id);
        IEnumerable<Series> GetSeries();
        Series GetSeriesByName(string name);
        int SaveSeries(Series series);
    }
}