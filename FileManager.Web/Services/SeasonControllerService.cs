using System;
using System.Collections.Generic;
using FileManager.Models;
using FileManager.BusinessLayer.Interfaces;
using FileManager.Web.Services.Interfaces;

namespace FileManager.Web.Services
{
    public class SeasonControllerService : ISeasonControllerService
    {
        private readonly IFileManagerObjectRepository<Season> _seasonRepository;

        public SeasonControllerService(IFileManagerObjectRepository<Season> seasonRepository)
        {
            _seasonRepository = seasonRepository;
        }

        public Season GetSeasonById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid SeasonId");

            return _seasonRepository.GetById(id);
        }

        public IEnumerable<Season> GetSeasons()
        {
            return _seasonRepository.Get();
        }

        public IEnumerable<Season> GetSeasonsByShowId(int showId)
        {
            if (showId <= 0)
                throw new ArgumentException("Invalid ShowId");

            return _seasonRepository.GetByParentId(showId);
        }

        public bool SaveSeason(Season season)
        {
            if (season == null)
                throw new ArgumentNullException(nameof(season));

            return _seasonRepository.Save(season);
        }
    }
}