using System.Collections.Generic;
using FileManager.Models;

namespace FileManager.Web.Services
{
    public interface ISeriesControllerService
    {
        IEnumerable<Series> GetSeries();
        bool SaveSeries(Series series);
    }
}