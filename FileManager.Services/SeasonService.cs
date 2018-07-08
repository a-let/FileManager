using System;
using System.Collections.Generic;

using Microsoft.Extensions.Configuration;

using FileManager.Models;
using FileManager.Services.Interfaces;

namespace FileManager.Services
{
    public class SeasonService : ISeasonService
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _seasonAddresses;
        private readonly IHttpClientFactory _httpClient;

        public SeasonService(IConfiguration configuration, IHttpClientFactory httpClient)
        {
            _configuration = configuration;
            _seasonAddresses = configuration.GetSection("SeasonAddresses");

            _httpClient = httpClient;
            _httpClient.BaseAddress = _configuration["FileManagerBaseAddress"];
        }

        public Season GetSeasonById(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentOutOfRangeException("SeasonId cannot be less than 1");

                Season season = null;

                var jsonString = _httpClient.GetAsync($"{_seasonAddresses["GetSeasonByIdAddress"]}/{id}").Result;
                season = _httpClient.DeserializeObject<Season>(jsonString);

                return season;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting season. {ex.Message}", ex);
            }
        }

        public IEnumerable<Season> GetSeasons()
        {
            try
            {
                IEnumerable<Season> seasonList = null;

                var jsonString = _httpClient.GetAsync(_seasonAddresses["GetSeasonsAddress"]).Result;
                seasonList = _httpClient.DeserializeObject<IEnumerable<Season>>(jsonString);

                return seasonList;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting seasons. {ex.Message}", ex);
            }
        }

        public IEnumerable<Season> GetSeasonsByShowId(int showId)
        {
            try
            {
                if (showId <= 0)
                    throw new ArgumentOutOfRangeException("SeasonId cannot be less than 1");

                IEnumerable<Season> seasonList = null;

                var jsonString = _httpClient.GetAsync($"{_seasonAddresses["GetSeasonsByShowIdAddress"]}/{showId}").Result;
                seasonList = _httpClient.DeserializeObject<IEnumerable<Season>>(jsonString);

                return seasonList;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting seasons. {ex.Message}", ex);
            }
        }

        public bool SaveSeason(Season season)
        {
            try
            {
                if (season == null)
                    throw new ArgumentNullException(nameof(season));

                bool success = false;

                var jsonString = _httpClient.PostAsync(season, _seasonAddresses["SaveSeasonAddress"]).Result;
                success = _httpClient.DeserializeObject<bool>(jsonString);

                return success;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error saving season. {ex.Message}", ex);
            }
        }
    }
}