using System.Collections.Generic;
using System.Threading.Tasks;
using FileManager.BusinessLayer;

namespace FileManager.Services.Interfaces
{
    public interface ISeasonService
    {
        Task<IEnumerable<Season>> GetSeasonsAsync();
        Task<bool> SaveSeasonAsync(Season season);
    }
}