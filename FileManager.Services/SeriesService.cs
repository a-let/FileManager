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
    public class SeriesService : ISeriesService
    {
        private readonly IConfigurationSection _seriesAddresses;
        private readonly HttpClient _httpClient;

        public SeriesService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _seriesAddresses = configuration.GetSection("SeriesAddresses");
            _httpClient = httpClientFactory.CreateClient("FileManager");
        }

        public async Task<IEnumerable<Series>> GetSeries()
        {
            try
            {
                var message = await _httpClient.GetAsync(_seriesAddresses["GetSeriesAddress"]);
                var seriesList = JsonConvert.DeserializeObject<IEnumerable<Series>>(await message.Content.ReadAsStringAsync());

                return seriesList;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting seriess. {ex.Message}", ex);
            }
        }

        public async Task<Series> GetSeriesById(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentOutOfRangeException("SeriesId cannot by less than 1");

                var message = await _httpClient.GetAsync($"{_seriesAddresses["GetSeriesByIdAddress"]}/{id}");
                var series = JsonConvert.DeserializeObject<Series>(await message.Content.ReadAsStringAsync());

                return series;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting series. {ex.Message}", ex);
            }
        }

        public async Task<Series> GetSeriesByName(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentNullException(nameof(name));

                var message = await _httpClient.GetAsync($"{_seriesAddresses["GetSeriesByNameAddress"]}/{name}");
                var series = JsonConvert.DeserializeObject<Series>(await message.Content.ReadAsStringAsync());

                return series;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting series. {ex.Message}", ex);
            }
        }

        public async Task<int> SaveSeries(Series series)
        {
            try
            {
                if (series == null)
                    throw new ArgumentNullException(nameof(series));

                var content = new StringContent(JsonConvert.SerializeObject(series), Encoding.UTF8, "application/json");
                var message = await _httpClient.PostAsync(_seriesAddresses["SaveSeriesAddress"], content);
                var id = JsonConvert.DeserializeObject<int>(await message.Content.ReadAsStringAsync());

                return id;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error saving series. {ex.Message}", ex);
            }
        }
    }
}