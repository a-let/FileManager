using System.Collections.Generic;
using FileManager.BusinessLayer;

namespace FileManager.Web.Services
{
    public interface ISeriesContollerService
    {
        IEnumerable<Series> GetSeries();
        bool SaveSeries(Series series);
    }
}