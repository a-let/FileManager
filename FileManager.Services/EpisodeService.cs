using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

using FileManager.BusinessLayer;
using FileManager.Services.Interfaces;
using System.Text;

namespace FileManager.Services
{
    public class EpisodeService : IEpisodeService
    {
        private readonly string FileManagerGetEpisodesAddress = "api/Episode";
        private readonly string FileManagerGetEpisodeByIdAddress = "api/Episode/id";
        private readonly string FileManagerGetEpisodeByNameAddress = "api/Episode/name";
        private readonly string FileManagerSaveEpisodeAddress = "api/Episode";
        private readonly string FileManagerGetEpisodeBySeasonId = "api/Episode/seasonid";

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

                    var response = await client.GetAsync(FileManagerGetEpisodesAddress);
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

                    var response = await client.GetAsync($"{FileManagerGetEpisodeByIdAddress}/{id}");
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

                    var response = await client.GetAsync($"{FileManagerGetEpisodeByNameAddress}/{name}");
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
            var success = false;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["FileManagerBaseAddress"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent(JsonConvert.SerializeObject(episode), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(FileManagerSaveEpisodeAddress, content);

                if(response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    success = JsonConvert.DeserializeObject<bool>(jsonString);
                }
            }

            return success;
        }

        public async Task<IEnumerable<Episode>> GetEpisodeBySeasonIdAsync(int seasonId)
        {
            try
            {
                IEnumerable<Episode> episodeList = null;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["FileManagerBaseAddress"]);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.GetAsync($"{FileManagerGetEpisodeBySeasonId}/{seasonId}");
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