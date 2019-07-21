using FileManager.Models;
using FileManager.Services.Interfaces;

using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FileManager.Services
{
    public class SeriesService : ServiceBase, ISeriesService
    {
        private readonly IConfigurationSection _seriesAddresses;

        public SeriesService(IConfiguration configuration, IHttpClientFactory httpClientFactory) : 
            base(httpClientFactory, "FileManager")
        {
            _seriesAddresses = configuration.GetSection("SeriesAddresses");
        }

        public async Task<IEnumerable<Series>> GetAsync()
        {
            try
            {
                var seriesList = await GetAsync<IEnumerable<Series>>(_seriesAddresses["GetSeriesAddress"]);
                return seriesList;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting seriess. {ex.Message}", ex);
            }
        }

        public async Task<Series> GetAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentOutOfRangeException("SeriesId cannot by less than 1");

                var series = await GetAsync<Series>($"{_seriesAddresses["GetSeriesByIdAddress"]}/{id}");
                return series;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting series. {ex.Message}", ex);
            }
        }

        public async Task<Series> GetAsync(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentNullException(nameof(name));

                var series = await GetAsync<Series>($"{_seriesAddresses["GetSeriesByNameAddress"]}/{name}");
                return series;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting series. {ex.Message}", ex);
            }
        }

        public async Task<int> SaveAsync(Series series)
        {
            try
            {
                if (series == null)
                    throw new ArgumentNullException(nameof(series));

                var id = await PostAsync<int>(_seriesAddresses["SaveSeriesAddress"], series);
                return id;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error saving series. {ex.Message}", ex);
            }
        }
    }
}