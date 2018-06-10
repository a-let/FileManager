using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

using FileManager.BusinessLayer;
using FileManager.Services.Interfaces;

namespace FileManager.Services
{
    public class EpisodeService : IEpisodeService
    {
        private readonly string FileManagerGetEpisodesAddress = "api/Episode";
        private readonly IConfiguration _configuration;

        public EpisodeService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<Episode>> GetEpisodesAsync()
        {            
            try
            {
                IEnumerable<Episode> episodeList = null;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["FileManagerBaseAddress"]);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.GetAsync(FileManagerGetEpisodesAddress);
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        episodeList = JsonConvert.DeserializeObject<IEnumerable<Episode>>(jsonString);
                    }
                }

                return episodeList;
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException($"Error getting episodes. {ex.Message}", ex);
            }
        }
    }
}