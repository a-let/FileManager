using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

using FileManager.Models;
using FileManager.Services.Interfaces;

namespace FileManager.Services
{
    public class SeriesService : ISeriesService
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _seriesAddresses;

        public SeriesService(IConfiguration configuration)
        {
            _configuration = configuration;
            _seriesAddresses = _configuration.GetSection("SeriesAddresses");
        }

        public async Task<IEnumerable<Series>> GetSeriesAsync()
        {
            try
            {
                IEnumerable<Series> seriesList = null;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["FIleManagerBaseAddress"]);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.GetAsync(_seriesAddresses["GetSeriesAddress"]);
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        seriesList = JsonConvert.DeserializeObject<IEnumerable<Series>>(jsonString);
                    }
                }

                return seriesList;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting seriess. {ex.Message}", ex);
            }
        }

        public async Task<Series> GetSeriesByIdAsync(int id)
        {
            try
            {
                Series series = null;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["FileManagerBaseAddress"]);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.GetAsync($"{_seriesAddresses["GetSeriesByIdAddress"]}/{id}");
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        series = JsonConvert.DeserializeObject<Series>(jsonString);
                    }
                }

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
                Series series = null;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["FileManagerBaseAddress"]);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.GetAsync($"{_seriesAddresses["GetSeriesByNameAddress"]}/{name}");
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        series = JsonConvert.DeserializeObject<Series>(jsonString);
                    }
                }

                return series;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting series. {ex.Message}", ex);
            }
        }

        public async Task<bool> SaveSeriesAsync(Series series)
        {
            try
            {
                bool success = false;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["FileManagerBaseAddress"]);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var content = new StringContent(JsonConvert.SerializeObject(series), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(_seriesAddresses["SaveSeriesAddress"], content);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        success = JsonConvert.DeserializeObject<bool>(jsonString);
                    }
                }

                return success;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error saving series. {ex.Message}", ex);
            }
        }
    }
}