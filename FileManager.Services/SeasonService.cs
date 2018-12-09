using FileManager.Models;
using FileManager.Services.Interfaces;

using Microsoft.Extensions.Configuration;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Services
{
    public class SeasonService : ISeasonService
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _seasonAddresses;
        private readonly HttpClient _httpClient;

        public SeasonService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _seasonAddresses = configuration.GetSection("SeasonAddresses");

            _httpClient = httpClientFactory.CreateClient("FileManager");
        }

        public async Task<Season> GetSeasonById(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentOutOfRangeException("SeasonId cannot be less than 1");

                var message = await _httpClient.GetAsync($"{_seasonAddresses["GetSeasonByIdAddress"]}/{id}");
                var season = JsonConvert.DeserializeObject<Season>(await message.Content.ReadAsStringAsync());

                return season;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting season. {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<Season>> GetSeasons()
        {
            try
            {
                var message = await _httpClient.GetAsync(_seasonAddresses["GetSeasonsAddress"]);
                var seasonList = JsonConvert.DeserializeObject<IEnumerable<Season>>(await message.Content.ReadAsStringAsync());

                return seasonList;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting seasons. {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<Season>> GetSeasonsByShowId(int showId)
        {
            try
            {
                if (showId <= 0)
                    throw new ArgumentOutOfRangeException("SeasonId cannot be less than 1");

                var message = await _httpClient.GetAsync($"{_seasonAddresses["GetSeasonsByShowIdAddress"]}/{showId}");
                var seasonList = JsonConvert.DeserializeObject<IEnumerable<Season>>(await message.Content.ReadAsStringAsync());

                return seasonList;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting seasons. {ex.Message}", ex);
            }
        }

        public async Task<int> SaveSeason(Season season)
        {
            try
            {
                if (season == null)
                    throw new ArgumentNullException(nameof(season));

                var content = new StringContent(JsonConvert.SerializeObject(season), Encoding.UTF8, "application/json");
                var message = await _httpClient.PostAsync(_seasonAddresses["SaveSeasonAddress"], content);
                var success = JsonConvert.DeserializeObject<int>(await message.Content.ReadAsStringAsync());

                return success;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error saving season. {ex.Message}", ex);
            }
        }
    }
}