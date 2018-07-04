using System;
using System.Collections.Generic;
using FileManager.Models;
using FileManager.BusinessLayer.Interfaces;

namespace FileManager.Web.Services
{
    public class SeasonControllerService : ISeasonControllerService
    {
        private readonly IFileManagerObjectAdapter<Season> _seasonAdapter;

        public SeasonControllerService(IFileManagerObjectAdapter<Season> seasonAdapter)
        {
            _seasonAdapter = seasonAdapter;
        }

        public Season GetSeasonById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid SeasonId");

            return _seasonAdapter.GetById(id);
        }

        public IEnumerable<Season> GetSeasons()
        {
            return _seasonAdapter.Get();
        }

        public IEnumerable<Season> GetSeasonsByShowId(int showId)
        {
            if (showId <= 0)
                throw new ArgumentException("Invalid ShowId");

            return _seasonAdapter.GetByParentId(showId);
        }

        public bool SaveSeason(Season season)
        {
            if (season == null)
                throw new ArgumentNullException(nameof(season));

            return _seasonAdapter.Save(season);
        }
    }
}