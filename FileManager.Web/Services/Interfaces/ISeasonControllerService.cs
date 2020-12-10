using FileManager.Interfaces;
using FileManager.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Web.Services.Interfaces
{
    public interface ISeasonControllerService : IService<Season>
    {
        IEnumerable<Season> GetSeasonsByShowId(int showId);
    }
}