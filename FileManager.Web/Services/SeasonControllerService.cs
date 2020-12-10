using FileManager.Models;
using FileManager.DataAccessLayer.Interfaces;
using FileManager.Web.Services.Interfaces;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Web.Services
{
    public class SeasonControllerService : ISeasonControllerService
    {
        private readonly ISeasonRepository _seasonRepository;

        public SeasonControllerService(ISeasonRepository seasonRepository)
        {
            _seasonRepository = seasonRepository;
        }

        public async Task<Season> GetAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid SeasonId");

            return await _seasonRepository.GetSeasonByIdAsync(id);
        }

        public async Task<IEnumerable<Season>> GetAsync()
        {
            return await Task.Run(() => _seasonRepository.GetSeasons());
        }

        public Task<Season> GetAsync(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Season> GetSeasonsByShowId(int showId)
        {
            if (showId <= 0)
                throw new ArgumentException("Invalid ShowId");

            return _seasonRepository.GetSeasonsByShowId(showId);
        }

        public async Task<int> SaveAsync(Season season)
        {
            if (season == null)
                throw new ArgumentNullException(nameof(season));

            return await _seasonRepository.SaveSeasonAsync(season);
        }
    }
}