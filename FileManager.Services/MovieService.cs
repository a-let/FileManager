using FileManager.Models;
using FileManager.Services.Interfaces;

using Logging;

using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;

namespace FileManager.Services
{
    public class MovieService : ServiceBase, IMovieService
    {        
        private readonly IConfigurationSection _movieAddresses;
        private readonly ILogger _logger;

        public MovieService(IConfiguration configuration, IHttpClientFactory httpClientFactory, ILogger logger) :
            base(httpClientFactory, "FileManager")
        {
            _movieAddresses = configuration.GetSection("MovieAddresses");
            _logger = logger;
        }

        public async Task<Movie> GetAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentOutOfRangeException("MovieId cannot be less than 1");

                var movie = await GetAsync<Movie>($"{_movieAddresses["GetMovieByIdAddress"]}/{id}");
                return movie;
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Error getting movie");
                throw;
            }
        }

        public async Task<Movie> GetAsync(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentNullException(nameof(name));

                var movie = await GetAsync<Movie>($"{_movieAddresses["GetMovieByNameAddress"]}/{name}");
                return movie;
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Error getting movie");
                throw;
            }
        }

        public async Task<IEnumerable<Movie>> GetAsync()
        {
            try
            {
                var movieList = await GetAsync<IEnumerable<Movie>>(_movieAddresses["GetMoviesAddress"]);
                return movieList;
            }
            catch(Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Error getting movies");
                throw;
            }
        }

        public async Task<IEnumerable<Movie>> GetMoviesBySeriesId(int seriesId)
        {
            try
            {
                if (seriesId <= 0)
                    throw new ArgumentOutOfRangeException("SeriesId cannot be less than 1");

                var movieList = await GetAsync<IEnumerable<Movie>>($"{_movieAddresses["GetMoviesBySeriesIdAddress"]}/{seriesId}");
                return movieList;
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Error getting movie");
                throw;
            }
        }

        public async Task<int> SaveAsync(Movie movie)
        {
            try
            {
                if (movie == null)
                    throw new ArgumentNullException(nameof(movie));

                var id = await PostAsync<int>(_movieAddresses["SaveMovieAddress"], movie);
                return id;
            }
            catch(Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Error saving movie");
                throw;
            }
        }
    }
}