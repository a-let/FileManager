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
    public class SeasonService : ISeasonService
    {
        //private readonly string GetSeasonsAddress = "api/Season";
        //private readonly string SaveSeasonAddress = "api/Season";

        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _seasonAddresses;

        public SeasonService(IConfiguration configuration)
        {
            _configuration = configuration;
            _seasonAddresses = configuration.GetSection("SeasonAddresses");
        }

        public async Task<Season> GetSeasonByIdAsync(int id)
        {
            try
            {
                Season season = null;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["FileManagerBaseAddress"]);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.GetAsync($"{_seasonAddresses["GetSeasonByIdAddress"]}/{id}");
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        season = JsonConvert.DeserializeObject<Season>(jsonString);
                    }
                }

                return season;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting season. {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<Season>> GetSeasonsAsync()
        {
            try
            {
                IEnumerable<Season> seasonList = null;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["FIleManagerBaseAddress"]);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.GetAsync(_seasonAddresses["GetSeasonsAddress"]);
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        seasonList = JsonConvert.DeserializeObject<IEnumerable<Season>>(jsonString);
                    }
                }

                return seasonList;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting seasons. {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<Season>> GetSeasonsByShowIdAsync(int showId)
        {
            try
            {
                IEnumerable<Season> seasonList = null;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["FIleManagerBaseAddress"]);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.GetAsync($"{_seasonAddresses["GetSeasonsByShowIdAddress"]}/{showId}");
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        seasonList = JsonConvert.DeserializeObject<IEnumerable<Season>>(jsonString);
                    }
                }

                return seasonList;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting seasons. {ex.Message}", ex);
            }
        }

        public async Task<bool> SaveSeasonAsync(Season season)
        {
            try
            {
                bool success = false;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["FileManagerBaseAddress"]);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var content = new StringContent(JsonConvert.SerializeObject(season), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(_seasonAddresses["SaveSeasonAddress"], content);

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
                throw new InvalidOperationException($"Error saving season. {ex.Message}", ex);
            }
        }
    }
}