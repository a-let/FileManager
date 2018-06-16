using System.Collections.Generic;
using System.Threading.Tasks;
using FileManager.Models;

namespace FileManager.Services.Interfaces
{
    public interface ISeasonService
    {
        Task<IEnumerable<Season>> GetSeasonsAsync();
        Task<bool> SaveSeasonAsync(Season season);
    }
}