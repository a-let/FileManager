using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

using FileManager.Models;
using FileManager.Services.Interfaces;
using System.Text;

namespace FileManager.Services
{
    public class MovieService : IMovieService
    {        
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _episodeAddresses;

        public MovieService(IConfiguration configuration)
        {
            _configuration = configuration;
            _episodeAddresses = _configuration.GetSection("MovieAddresses");
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            try
            {
                Movie movie = null;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["FileManagerBaseAddress"]);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.GetAsync($"{_episodeAddresses["GetMovieByIdAddress"]}/{id}");
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        movie = JsonConvert.DeserializeObject<Movie>(jsonString);
                    }
                }

                return movie;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting movie. {ex.Message}", ex);
            }
        }

        public async Task<Movie> GetMovieByNameAsync(string name)
        {
            try
            {
                Movie movie = null;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["FileManagerBaseAddress"]);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.GetAsync($"{_episodeAddresses["GetMovieByNameAddress"]}/{name}");
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        movie = JsonConvert.DeserializeObject<Movie>(jsonString);
                    }
                }

                return movie;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting movie. {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<Movie>> GetMoviesAsync()
        {
            try
            {
                IEnumerable<Movie> movieList = null;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["FIleManagerBaseAddress"]);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.GetAsync(_episodeAddresses["GetMoviesAddress"]);
                    if(response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        movieList = JsonConvert.DeserializeObject<IEnumerable<Movie>>(jsonString);
                    }
                }

                return movieList;
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException($"Error getting movies. {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<Movie>> GetMoviesBySeriesId(int seriesId)
        {
            try
            {
                IEnumerable<Movie> movieList = null;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["FileManagerBaseAddress"]);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.GetAsync($"{_episodeAddresses["GetMoviesBySeriesIdAddress"]}/{seriesId}");
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        movieList = JsonConvert.DeserializeObject<IEnumerable<Movie>>(jsonString);
                    }
                }

                return movieList;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting movie. {ex.Message}", ex);
            }
        }

        public async Task<bool> SaveMovieAsync(Movie movie)
        {
            try
            {
                bool success = false;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["FileManagerBaseAddress"]);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var content = new StringContent(JsonConvert.SerializeObject(movie), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(_episodeAddresses["SaveMovieAddress"], content);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        success = JsonConvert.DeserializeObject<bool>(jsonString);
                    }
                }

                return success;
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException($"Error saving movie. {ex.Message}", ex);
            }
        }
    }
}