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
    public class ShowService : ServiceBase, IShowService
    {
        private readonly IConfigurationSection _showAddresses;
        private readonly ILogger<ShowService> _logger;

        public ShowService(IConfiguration configuration, IHttpClientFactory httpClientFactory, ILogger<ShowService> logger) :
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
                _logger.LogError(ex, "Error getting show");
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
                _logger.LogError(ex, "Error getting show");
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
                _logger.LogError(ex, "Error getting shows");
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
                _logger.LogError(ex, $"Error saving show");
                throw;
            }
        }
    }
}