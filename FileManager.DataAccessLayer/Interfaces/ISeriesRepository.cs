using FileManager.Models;
using System;
using System.Collections.Generic;

namespace FileManager.DataAccessLayer.Interfaces
{
    public interface ISeriesRepository : IDisposable
    {
        Series GetSeriesById(int id);
        IEnumerable<Series> GetSeries();
        Series GetSeriesByName(string name);
        int SaveSeries(Series series);
    }
}
