using FileManager.Models;
using FileManager.Services.Interfaces;

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

        public ShowService(IConfiguration configuration, IHttpClientFactory httpClientFactory) :
            base(httpClientFactory, "FileManager")
        {
            _showAddresses = configuration.GetSection("ShowAddresses");
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
                throw new InvalidOperationException($"Error getting movie. {ex.Message}", ex);
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
                throw new InvalidOperationException($"Error getting movie. {ex.Message}", ex);
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
                throw new InvalidOperationException($"Error getting shows. {ex.Message}", ex);
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
                throw new InvalidOperationException($"Error saving show. {ex.Message}", ex);
            }
        }
    }
}