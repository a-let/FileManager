using FileManager.Models;
using FileManager.Services.Interfaces;

using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FileManager.Services
{
    public class SeasonService : ServiceBase, ISeasonService
    {
        private readonly IConfigurationSection _seasonAddresses;

        public SeasonService(IConfiguration configuration, IHttpClientFactory httpClientFactory) :
            base(httpClientFactory, "FileManager")
        {
            _seasonAddresses = configuration.GetSection("SeasonAddresses");
        }

        public async Task<Season> GetAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentOutOfRangeException("SeasonId cannot be less than 1");

                var season = await GetAsync<Season>($"{_seasonAddresses["GetSeasonByIdAddress"]}/{id}");
                return season;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting season. {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<Season>> GetAsync()
        {
            try
            {
                var seasonList = await GetAsync<IEnumerable<Season>>(_seasonAddresses["GetSeasonsAddress"]);
                return seasonList;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting seasons. {ex.Message}", ex);
            }
        }

        public Task<Season> GetAsync(string name) => throw new NotImplementedException();

        public async Task<IEnumerable<Season>> GetSeasonsByShowId(int showId)
        {
            try
            {
                if (showId <= 0)
                    throw new ArgumentOutOfRangeException("SeasonId cannot be less than 1");

                var seasonList = await GetAsync<IEnumerable<Season>>($"{_seasonAddresses["GetSeasonsByShowIdAddress"]}/{showId}");
                return seasonList;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting seasons. {ex.Message}", ex);
            }
        }

        public async Task<int> SaveAsync(Season season)
        {
            try
            {
                if (season == null)
                    throw new ArgumentNullException(nameof(season));

                var id = await PostAsync<int>(_seasonAddresses["SaveSeasonAddress"], season);
                return id;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error saving season. {ex.Message}", ex);
            }
        }
    }
}