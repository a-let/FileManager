using FileManager.Models;
using FileManager.Services.Interfaces;

using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FileManager.Services
{
    public class EpisodeService : ServiceBase, IEpisodeService
    {
        private readonly IConfigurationSection _episodeAddresses;

        public EpisodeService(IConfiguration configuration, IHttpClientFactory httpClientFactory) :
            base(httpClientFactory, "FileManager")
        {
            _episodeAddresses = configuration.GetSection("EpisodeAddresses");
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
                throw new InvalidOperationException($"Error getting episodes. {ex.Message}", ex);
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
                throw new InvalidOperationException($"Error getting episode. {ex.Message}", ex);
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
                throw new InvalidOperationException($"Error getting episodes. {ex.Message}", ex);
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
                throw new InvalidOperationException($"Error saving episode. {ex.Message}", ex);
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
                throw new InvalidOperationException($"Error getting episodes. {ex.Message}", ex);
            }
        }
    }
}