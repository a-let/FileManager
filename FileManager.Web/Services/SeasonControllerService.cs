using FileManager.Models;
using FileManager.DataAccessLayer.Interfaces;
using FileManager.Web.Services.Interfaces;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Web.Services
{
    public class SeasonControllerService : IControllerService<Season>
    {
        private readonly IRepository<Season> _seasonRepository;

        public SeasonControllerService(IRepository<Season> seasonRepository)
        {
            _seasonRepository = seasonRepository;
        }

        public async Task<Season> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid SeasonId");

            return await _seasonRepository.GetByIdAsync(id);
        }

        public IEnumerable<Season> Get() => 
            _seasonRepository.Get();

        public Season GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task SaveAsync(Season season)
        {
            if (season == null)
                throw new ArgumentNullException(nameof(season));

            await _seasonRepository.SaveAsync(season);
        }
    }
}