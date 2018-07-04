using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

using FileManager.Models;
using FileManager.Services.Interfaces;
using System.Text;

namespace FileManager.Services
{
    public class EpisodeService : IEpisodeService
    {
        private readonly string GetEpisodesAddress = "api/Episode";
        private readonly string GetEpisodeByIdAddress = "api/Episode/id";
        private readonly string GetEpisodeByNameAddress = "api/Episode/name";
        private readonly string SaveEpisodeAddress = "api/Episode";
        private readonly string GetEpisodeBySeasonId = "api/Episode/seasonid";

        private readonly IConfiguration _configuration;

        public EpisodeService(IConfiguration configuration)
        {
            _configuration = configuration;
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

                    var response = await client.GetAsync(GetEpisodesAddress);
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

                    var response = await client.GetAsync($"{GetEpisodeByIdAddress}/{id}");
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

                    var response = await client.GetAsync($"{GetEpisodeByNameAddress}/{name}");
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
                    var response = await client.PostAsync(SaveEpisodeAddress, content);

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

                    var response = await client.GetAsync($"{GetEpisodeBySeasonId}/{seasonId}");
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