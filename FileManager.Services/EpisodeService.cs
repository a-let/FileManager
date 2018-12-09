using FileManager.Models;
using FileManager.Services.Interfaces;

using Microsoft.Extensions.Configuration;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;

namespace FileManager.Services
{
    public class EpisodeService : IEpisodeService
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _episodeAddresses;
        private readonly HttpClient _httpClient;

        public EpisodeService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _episodeAddresses = _configuration.GetSection("EpisodeAddresses");

            _httpClient = httpClientFactory.CreateClient("FileManager");
        }

        public async Task<IEnumerable<Episode>> GetEpisodes()
        {
            try
            {
                var message = await _httpClient.GetAsync(_episodeAddresses["GetEpisodesAddress"]);
                var episodeList = JsonConvert.DeserializeObject<IEnumerable<Episode>>(await message.Content.ReadAsStringAsync());

                return episodeList;
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException($"Error getting episodes. {ex.Message}", ex);
            }
        }

        public async Task<Episode> GetEpisodeById(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentOutOfRangeException("EpisodeId cannot be less than 1");

                var message = await _httpClient.GetAsync($"{_episodeAddresses["GetEpisodeByIdAddress"]}/{id}");
                var episode = JsonConvert.DeserializeObject<Episode>(await message.Content.ReadAsStringAsync());

                return episode;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting episode. {ex.Message}", ex);
            }
        }

        public async Task<Episode> GetEpisodeByName(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentNullException(nameof(name));

                var message = await _httpClient.GetAsync($"{_episodeAddresses["GetEpisodeByNameAddress"]}/{name}");
                var episode = JsonConvert.DeserializeObject<Episode>(await message.Content.ReadAsStringAsync());

                return episode;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting episodes. {ex.Message}", ex);
            }
        }

        public async Task<int> SaveEpisode(Episode episode)
        {
            try
            {
                if (episode == null)
                    throw new ArgumentNullException(nameof(episode));

                var content = new StringContent(JsonConvert.SerializeObject(episode), Encoding.UTF8, "application/json");
                var message = await _httpClient.PostAsync(_episodeAddresses["SaveEpisodeAddress"], content);
                var id = JsonConvert.DeserializeObject<int>(await message.Content.ReadAsStringAsync());

                return id;
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException($"Error saving episode. {ex.Message}", ex);
            }
            
        }

        public async Task<IEnumerable<Episode>> GetEpisodesBySeasonId(int seasonId)
        {
            try
            {
                if (seasonId <= 0)
                    throw new ArgumentOutOfRangeException("SeasonId cannot be less than 1");

                var message = await _httpClient.GetAsync($"{_episodeAddresses["GetEpisodeBySeasonIdAddress"]}/{seasonId}");
                var episodeList = JsonConvert.DeserializeObject<IEnumerable<Episode>>(await message.Content.ReadAsStringAsync());

                return episodeList;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting episodes. {ex.Message}", ex);
            }
        }
    }
}