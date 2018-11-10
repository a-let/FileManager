using FileManager.Models;
using FileManager.DataAccessLayer.Interfaces;
using FileManager.Web.Services.Interfaces;

using System;
using System.Collections.Generic;

namespace FileManager.Web.Services
{
    public class SeasonControllerService : ISeasonControllerService
    {
        private readonly ISeasonRepository _seasonRepository;

        public SeasonControllerService(ISeasonRepository seasonRepository)
        {
            _seasonRepository = seasonRepository;
        }

        public Season GetSeasonById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid SeasonId");

            return _seasonRepository.GetSeasonById(id);
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

        public int SaveSeason(Season season)
        {
            if (season == null)
                throw new ArgumentNullException(nameof(season));

            return _seasonRepository.SaveSeason(season);
        }
    }
}