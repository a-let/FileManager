using FileManager.DataAccessLayer.Interfaces;
using FileManager.Models;
using System.Collections.Generic;
using System.Linq;

namespace FileManager.Tests.Mocks
{
    public class MockSeasonRepository : ISeasonRepository
    { 
        public IEnumerable<Season> GetSeasons()
        {
            return new List<Season>();
        }

        public Season GetSeasonById(int id)
        {
            return new Season();
        }

        public IQueryable<Season> GetSeasonsByShowId(int showId)
        {
            return new List<Season>().AsQueryable();
        }

        public bool SaveSeason(Season season)
        {
            return season != null;
        }

        public void Dispose()
        {

        }
    }
}