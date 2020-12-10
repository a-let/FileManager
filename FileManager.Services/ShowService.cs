using FileManager.Interfaces;
using FileManager.Models;

using Logging;

using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FileManager.Services
{
    public class ShowService : ServiceBase, IShowService
    {
        private readonly IConfigurationSection _showAddresses;
        private readonly ILogger _logger;

        public ShowService(IConfiguration configuration, IHttpClientFactory httpClientFactory, ILogger logger) :
            base(httpClientFactory, "FileManager")
        {
            _showAddresses = configuration.GetSection("ShowAddresses");
            _logger = logger;
        }

        public async Task<Show> GetAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentOutOfRangeException("ShowId cannot be less than 1");

                var show = await GetAsync<Show>($"{_showAddresses["GetShowByIdAddress"]}/{id}");

                return show;
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Error getting show - {ex.Message}");
                throw;
            }
        }

        public async Task<Show> GetAsync(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentNullException(nameof(name));

                var show = await GetAsync<Show>($"{_showAddresses["GetShowByNameAddress"]}/{name}");
                return show;
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Error getting show - {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Show>> GetAsync()
        {
            try
            {
                var showList = await GetAsync<IEnumerable<Show>>(_showAddresses["GetShowsAddress"]);
                return showList;
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Error getting shows - {ex.Message}");
                throw;
            }
        }

        public async Task<int> SaveAsync(Show show)
        {
            try
            {
                if (show == null)
                    throw new ArgumentNullException(nameof(show));

                var id = await PostAsync<int>(_showAddresses["SaveShowAddress"], show);

                return id;
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Error saving show - {ex.Message}");
                throw;
            }
        }
    }
}