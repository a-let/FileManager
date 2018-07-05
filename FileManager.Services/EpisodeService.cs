using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text;

using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

using FileManager.Models;
using FileManager.Services.Interfaces;

namespace FileManager.Services
{
    public class EpisodeService : IEpisodeService
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _episodeAddresses;

        public EpisodeService(IConfiguration configuration)
        {
            _configuration = configuration;
            _episodeAddresses = _configuration.GetSection("EpisodeAddresses");
        }

        public async Task<IEnumerable<Episode>> GetEpisodesAsync()
        {            
            try
            {
                IEnumerable<Episode> episodeList = null;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["FileManagerBaseAddress"]);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.GetAsync(_episodeAddresses["GetEpisodesAddress"]);
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        episodeList = JsonConvert.DeserializeObject<IEnumerable<Episode>>(jsonString);
                    }
                }

                return episodeList;
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException($"Error getting episodes. {ex.Message}", ex);
            }
        }

        public async Task<Episode> GetEpisodeByIdAsync(int id)
        {
            try
            {
                Episode episode = null;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["FileManagerBaseAddress"]);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.GetAsync($"{_episodeAddresses["GetEpisodeByIdAddress"]}/{id}");
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        episode = JsonConvert.DeserializeObject<Episode>(jsonString);
                    }
                }

                return episode;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting episode. {ex.Message}", ex);
            }
        }

        public async Task<Episode> GetEpisodeByNameAsync(string name)
        {
            try
            {
                Episode episode = null;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["FileManagerBaseAddress"]);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.GetAsync($"{_episodeAddresses["GetEpisodeByNameAddress"]}/{name}");
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        episode = JsonConvert.DeserializeObject<Episode>(jsonString);
                    }
                }

                return episode;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting episodes. {ex.Message}", ex);
            }
        }

        public async Task<bool> SaveEpisodeAsync(Episode episode)
        {
            try
            {
                var success = false;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["FileManagerBaseAddress"]);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var content = new StringContent(JsonConvert.SerializeObject(episode), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(_episodeAddresses["SaveEpisodeAddress"], content);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        success = JsonConvert.DeserializeObject<bool>(jsonString);
                    }
                }

                return success;
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException($"Error saving episode. {ex.Message}", ex);
            }
            
        }

        public async Task<IEnumerable<Episode>> GetEpisodesBySeasonIdAsync(int seasonId)
        {
            try
            {
                IEnumerable<Episode> episodeList = null;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["FileManagerBaseAddress"]);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.GetAsync($"{_episodeAddresses["GetEpisodeBySeasonIdAddress"]}/{seasonId}");
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        episodeList = JsonConvert.DeserializeObject<IEnumerable<Episode>>(jsonString);
                    }
                }

                return episodeList;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting episodes. {ex.Message}", ex);
            }
        }
    }
}