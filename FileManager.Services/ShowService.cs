using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

using FileManager.BusinessLayer;
using FileManager.Services.Interfaces;

namespace FileManager.Services
{
    public class ShowService : IShowService
    {
        private readonly string GetShowsAddress = "api/Show";
        private readonly string SaveShowAddress = "api/Show";

        private readonly IConfiguration _configuration;

        public ShowService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<Show>> GetShowsAsync()
        {
            try
            {
                IEnumerable<Show> showList = null;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["FIleManagerBaseAddress"]);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.GetAsync(GetShowsAddress);
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        showList = JsonConvert.DeserializeObject<IEnumerable<Show>>(jsonString);
                    }
                }

                return showList;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting shows. {ex.Message}", ex);
            }
        }

        public async Task<bool> SaveShowAsync(Show show)
        {
            try
            {
                bool success = false;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["FileManagerBaseAddress"]);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var content = new StringContent(JsonConvert.SerializeObject(show), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(SaveShowAddress, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        success = JsonConvert.DeserializeObject<bool>(jsonString);
                    }
                }

                return success;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error saving show. {ex.Message}", ex);
            }
        }
    }
}