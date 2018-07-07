using System;
using System.Collections.Generic;

using Microsoft.Extensions.Configuration;

using FileManager.Models;
using FileManager.Services.Interfaces;

namespace FileManager.Services
{
    public class EpisodeService : IEpisodeService
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _episodeAddresses;
        private readonly IHttpClientFactory _httpClient;

        public EpisodeService(IConfiguration configuration, IHttpClientFactory httpClient)
        {
            _configuration = configuration;
            _episodeAddresses = _configuration.GetSection("EpisodeAddresses");
            _httpClient = httpClient;
            _httpClient.BaseAddress = _configuration["FileManagerBaseAddress"];
        }

        public IEnumerable<Episode> GetEpisodes()
        {
            try
            {
                IEnumerable<Episode> episodeList = null;

                var jsonString = _httpClient.GetAsync(_episodeAddresses["GetEpisodesAddress"]).Result;
                episodeList = _httpClient.DeserializeObject<IEnumerable<Episode>>(jsonString);

                return episodeList;
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException($"Error getting episodes. {ex.Message}", ex);
            }
        }

        public Episode GetEpisodeById(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentOutOfRangeException("EpisodeId cannot be less than 1");

                Episode episode = null;

                var jsonString = _httpClient.GetAsync($"{_episodeAddresses["GetEpisodeByIdAddress"]}/{id}").Result;
                episode = _httpClient.DeserializeObject<Episode>(jsonString);

                return episode;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting episode. {ex.Message}", ex);
            }
        }

        public Episode GetEpisodeByName(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentNullException(nameof(name));

                Episode episode = null;

                var jsonString = _httpClient.GetAsync($"{_episodeAddresses["GetEpisodeByNameAddress"]}/{name}").Result;
                episode = _httpClient.DeserializeObject<Episode>(jsonString);

                return episode;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting episodes. {ex.Message}", ex);
            }
        }

        public bool SaveEpisode(Episode episode)
        {
            try
            {
                if (episode == null)
                    throw new ArgumentNullException(nameof(episode));

                var success = false;

                var jsonString = _httpClient.PostAsync(episode, _episodeAddresses["SaveEpisodeAddress"]).Result;
                success = _httpClient.DeserializeObject<bool>(jsonString);

                return success;
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException($"Error saving episode. {ex.Message}", ex);
            }
            
        }

        public IEnumerable<Episode> GetEpisodesBySeasonId(int seasonId)
        {
            try
            {
                if (seasonId <= 0)
                    throw new ArgumentOutOfRangeException("SeasonId cannot be less than 1");

                IEnumerable<Episode> episodeList = null;

                var jsonString = _httpClient.GetAsync($"{_episodeAddresses["GetEpisodeBySeasonIdAddress"]}/{seasonId}").Result;
                episodeList = _httpClient.DeserializeObject<IEnumerable<Episode>>(jsonString);

                return episodeList;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting episodes. {ex.Message}", ex);
            }
        }
    }
}