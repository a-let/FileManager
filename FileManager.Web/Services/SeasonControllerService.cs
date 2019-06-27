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

        public async Task<Season> GetSeasonByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid SeasonId");

            return await _seasonRepository.GetSeasonByIdAsync(id);
        }

        public IEnumerable<Season> GetSeasons()
        {
            return _seasonRepository.GetSeasons();
        }

        public IEnumerable<Season> GetSeasonsByShowId(int showId)
        {
            if (showId <= 0)
                throw new ArgumentException("Invalid ShowId");

            return _seasonRepository.GetSeasonsByShowId(showId);
        }

        public async Task<int> SaveSeasonAsync(Season season)
        {
            if (season == null)
                throw new ArgumentNullException(nameof(season));

            return await _seasonRepository.SaveSeasonAsync(season);
        }
    }
}