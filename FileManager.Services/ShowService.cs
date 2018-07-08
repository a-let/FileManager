using System;
using System.Collections.Generic;

using Microsoft.Extensions.Configuration;

using FileManager.Models;
using FileManager.Services.Interfaces;

namespace FileManager.Services
{
    public class ShowService : IShowService
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _showAddresses;
        private readonly IHttpClientFactory _httpClient;

        public ShowService(IConfiguration configuration, IHttpClientFactory httpClient)
        {
            _configuration = configuration;
            _showAddresses = _configuration.GetSection("ShowAddresses");

            _httpClient = httpClient;
            _httpClient.BaseAddress = _configuration["FileManagerBaseAddress"];
        }

        public Show GetShowById(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentOutOfRangeException("ShowId cannot be less than 1");

                Show show = null;

                var jsonString = _httpClient.GetAsync($"{_showAddresses["GetShowByIdAddress"]}/{id}").Result;
                show = _httpClient.DeserializeObject<Show>(jsonString);

                return show;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting movie. {ex.Message}", ex);
            }
        }

        public Show GetShowByName(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentNullException(nameof(name));

                Show show = null;

                var jsonString = _httpClient.GetAsync($"{_showAddresses["GetShowByNameAddress"]}/{name}").Result;
                show = _httpClient.DeserializeObject<Show>(jsonString);

                return show;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting movie. {ex.Message}", ex);
            }
        }

        public IEnumerable<Show> GetShows()
        {
            try
            {
                IEnumerable<Show> showList = null;

                var jsonString = _httpClient.GetAsync(_showAddresses["GetShowsAddress"]).Result;
                showList = _httpClient.DeserializeObject<IEnumerable<Show>>(jsonString);

                return showList;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting shows. {ex.Message}", ex);
            }
        }

        public bool SaveShow(Show show)
        {
            try
            {
                if (show == null)
                    throw new ArgumentNullException(nameof(show));

                bool success = false;

                var jsonString = _httpClient.PostAsync(show, _showAddresses["SaveShowAddress"]).Result;
                success = _httpClient.DeserializeObject<bool>(jsonString);

                return success;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error saving show. {ex.Message}", ex);
            }
        }
    }
}