using System.Collections.Generic;

using FileManager.Models;
using FileManager.Web.Services.Interfaces;

namespace FileManager.Tests.Mocks
{
    public class MockSeasonControllerService : ISeasonControllerService
    {
        public Season GetSeasonById(int id)
        {
            return id != 1 ? null : new Season
            {
                SeasonId = 1
            };
        }

        public IEnumerable<Season> GetSeasons()
        {
            return new List<Season>();
        }

        public IEnumerable<Season> GetSeasonsByShowId(int showId)
        {
            return new List<Season>();
        }

        public int SaveSeason(Season season)
        {
            return season != null ? 1 : 0;
        }
    }
}