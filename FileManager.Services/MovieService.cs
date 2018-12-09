using FileManager.Models;
using FileManager.Services.Interfaces;

using Microsoft.Extensions.Configuration;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Net.Http;

namespace FileManager.Services
{
    public class MovieService : IMovieService
    {        
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _movieAddresses;
        private readonly HttpClient _httpClient;

        public MovieService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _movieAddresses = _configuration.GetSection("MovieAddresses");

            _httpClient = httpClientFactory.CreateClient("FileManager");
        }

        public async Task<Movie> GetMovieById(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentOutOfRangeException("MovieId cannot be less than 1");

                var message = await _httpClient.GetAsync($"{_movieAddresses["GetMovieByIdAddress"]}/{id}");
                var movie = JsonConvert.DeserializeObject<Movie>(await message.Content.ReadAsStringAsync());
                                
                return movie;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting movie. {ex.Message}", ex);
            }
        }

        public async Task<Movie> GetMovieByName(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentNullException(nameof(name));

                var message = await _httpClient.GetAsync($"{_movieAddresses["GetMovieByNameAddress"]}/{name}");
                var movie = JsonConvert.DeserializeObject<Movie>(await message.Content.ReadAsStringAsync());

                return movie;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting movie. {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<Movie>> GetMovies()
        {
            try
            {
                var message = await _httpClient.GetAsync(_movieAddresses["GetMoviesAddress"]);
                var movieList = JsonConvert.DeserializeObject<IEnumerable<Movie>>(await message.Content.ReadAsStringAsync());

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
                if (seriesId <= 0)
                    throw new ArgumentOutOfRangeException("SeriesId cannot be less than 1");

                var message = await _httpClient.GetAsync($"{_movieAddresses["GetMoviesBySeriesIdAddress"]}/{seriesId}");
                var movieList = JsonConvert.DeserializeObject<IEnumerable<Movie>>(await message.Content.ReadAsStringAsync());

                return movieList;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting movie. {ex.Message}", ex);
            }
        }

        public async Task<int> SaveMovie(Movie movie)
        {
            try
            {
                if (movie == null)
                    throw new ArgumentNullException(nameof(movie));

                var content = new StringContent(JsonConvert.SerializeObject(movie), Encoding.UTF8, "application/json");
                var message = await _httpClient.PostAsync(_movieAddresses["SaveMovieAddress"], content);
                var id = JsonConvert.DeserializeObject<int>(await message.Content.ReadAsStringAsync());

                return id;
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException($"Error saving movie. {ex.Message}", ex);
            }
        }
    }
}