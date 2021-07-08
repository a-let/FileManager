using FileManager.Interfaces;
using FileManager.Models;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FileManager.Services
{
    internal class EpisodeService : ServiceBase, IEpisodeService
    {
        private readonly IConfigurationSection _episodeAddresses;
        private readonly ILogger<EpisodeService> _logger;

        public EpisodeService(IConfiguration configuration, IHttpClientFactory httpClientFactory, ILogger<EpisodeService> logger) :
            base(httpClientFactory, "FileManager")
        {
            _episodeAddresses = configuration.GetSection("EpisodeAddresses");
            _logger = logger;
        }

        public async Task<IEnumerable<Episode>> GetAsync()
        {
            try
            {
                var episodeList = await GetAsync<IEnumerable<Episode>>(_episodeAddresses["GetEpisodesAddress"]);
                return episodeList;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error getting episodes");
                throw;
            }
        }

        public async Task<Episode> GetAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentOutOfRangeException("EpisodeId cannot be less than 1");

                var episode = await GetAsync<Episode>($"{_episodeAddresses["GetEpisodeByIdAddress"]}/{id}");
                return episode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting episode");
                throw;
            }
        }

        public async Task<Episode> GetAsync(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentNullException(nameof(name));

                var episode = await GetAsync<Episode>($"{_episodeAddresses["GetEpisodeByNameAddress"]}/{name}");
                return episode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting episodes");
                throw;
            }
        }

        public async Task<int> SaveAsync(Episode episode)
        {
            try
            {
                if (episode == null)
                    throw new ArgumentNullException(nameof(episode));

                var id = await PostAsync<int>(_episodeAddresses["SaveEpisodeAddress"], episode);
                return id;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error saving episode");
                throw;
            }
            
        }

        public async Task<IEnumerable<Episode>> GetEpisodesBySeasonId(int seasonId)
        {
            try
            {
                if (seasonId <= 0)
                    throw new ArgumentOutOfRangeException("SeasonId cannot be less than 1");

                var episodeList = await GetAsync<IEnumerable<Episode>>($"{_episodeAddresses["GetEpisodeBySeasonIdAddress"]}/{seasonId}");
                return episodeList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting episodes");
                throw;
            }
        }
    }
}