using System;
using System.Collections.Generic;

using Microsoft.Extensions.Configuration;

using FileManager.Models;
using FileManager.Services.Interfaces;

namespace FileManager.Services
{
    public class SeriesService : ISeriesService
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _seriesAddresses;
        private readonly IHttpClientFactory _httpClient;

        public SeriesService(IConfiguration configuration, IHttpClientFactory httpClient)
        {
            _configuration = configuration;
            _seriesAddresses = _configuration.GetSection("SeriesAddresses");

            _httpClient = httpClient;
            _httpClient.BaseAddress = _configuration["FIleManagerBaseAddress"];
        }

        public IEnumerable<Series> GetSeries()
        {
            try
            {
                IEnumerable<Series> seriesList = null;

                var jsonString = _httpClient.GetAsync(_seriesAddresses["GetSeriesAddress"]).Result;
                seriesList = _httpClient.DeserializeObject<IEnumerable<Series>>(jsonString);

                return seriesList;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting seriess. {ex.Message}", ex);
            }
        }

        public Series GetSeriesById(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentOutOfRangeException("SeriesId cannot by less than 1");

                Series series = null;

                var jsonString = _httpClient.GetAsync($"{_seriesAddresses["GetSeriesByIdAddress"]}/{id}").Result;
                series = _httpClient.DeserializeObject<Series>(jsonString);

                return series;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting series. {ex.Message}", ex);
            }
        }

        public Series GetSeriesByName(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentNullException(nameof(name));

                Series series = null;

                var jsonString = _httpClient.GetAsync($"{_seriesAddresses["GetSeriesByNameAddress"]}/{name}").Result;
                series = _httpClient.DeserializeObject<Series>(jsonString);

                return series;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting series. {ex.Message}", ex);
            }
        }

        public bool SaveSeries(Series series)
        {
            try
            {
                if (series == null)
                    throw new ArgumentNullException(nameof(series));

                bool success = false;

                var jsonString = _httpClient.PostAsync(series, _seriesAddresses["SaveSeriesAddress"]).Result;
                success = _httpClient.DeserializeObject<bool>(jsonString);

                return success;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error saving series. {ex.Message}", ex);
            }
        }
    }
}