using FileManager.Models;
using FileManager.Services.Interfaces;

using Microsoft.Extensions.Configuration;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Services
{
    public class ShowService : IShowService
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _showAddresses;
        private readonly HttpClient _httpClient;

        public ShowService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _showAddresses = _configuration.GetSection("ShowAddresses");

            _httpClient = httpClientFactory.CreateClient("FileManager");
        }

        public async Task<Show> GetShowById(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentOutOfRangeException("ShowId cannot be less than 1");

                var message = await _httpClient.GetAsync($"{_showAddresses["GetShowByIdAddress"]}/{id}");
                var show = JsonConvert.DeserializeObject<Show>(await message.Content.ReadAsStringAsync());

                return show;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting movie. {ex.Message}", ex);
            }
        }

        public async Task<Show> GetShowByName(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentNullException(nameof(name));

                var message = await _httpClient.GetAsync($"{_showAddresses["GetShowByNameAddress"]}/{name}");
                var show = JsonConvert.DeserializeObject<Show>(await message.Content.ReadAsStringAsync());

                return show;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting movie. {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<Show>> GetShows()
        {
            try
            {
                var message = await _httpClient.GetAsync(_showAddresses["GetShowsAddress"]);
                var showList = JsonConvert.DeserializeObject<IEnumerable<Show>>(await message.Content.ReadAsStringAsync());

                return showList;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting shows. {ex.Message}", ex);
            }
        }

        public async Task<int> SaveShow(Show show)
        {
            try
            {
                if (show == null)
                    throw new ArgumentNullException(nameof(show));

                var content = new StringContent(JsonConvert.SerializeObject(show), Encoding.UTF8, "application/json");
                var message = await _httpClient.PostAsync(_showAddresses["SaveShowAddress"], content);
                var id = JsonConvert.DeserializeObject<int>(await message.Content.ReadAsStringAsync());

                return id;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error saving show. {ex.Message}", ex);
            }
        }
    }
}