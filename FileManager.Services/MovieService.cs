using System;
using System.Collections.Generic;

using Microsoft.Extensions.Configuration;

using FileManager.Models;
using FileManager.Services.Interfaces;

namespace FileManager.Services
{
    public class MovieService : IMovieService
    {        
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _movieAddresses;
        private readonly IHttpClientFactory _httpClient;

        public MovieService(IConfiguration configuration, IHttpClientFactory httpClient)
        {
            _configuration = configuration;
            _movieAddresses = _configuration.GetSection("MovieAddresses");

            _httpClient = httpClient;
            _httpClient.BaseAddress = _configuration["FileManagerBaseAddress"];
        }

        public Movie GetMovieById(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentOutOfRangeException("MovieId cannot be less than 1");

                Movie movie = null;

                var jsonString = _httpClient.GetAsync($"{_movieAddresses["GetMovieByIdAddress"]}/{id}").Result;
                movie = _httpClient.DeserializeObject<Movie>(jsonString);
                                
                return movie;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting movie. {ex.Message}", ex);
            }
        }

        public Movie GetMovieByName(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentNullException(nameof(name));

                Movie movie = null;

                var jsonString = _httpClient.GetAsync($"{_movieAddresses["GetMovieByNameAddress"]}/{name}").Result;
                movie = _httpClient.DeserializeObject<Movie>(jsonString);

                return movie;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting movie. {ex.Message}", ex);
            }
        }

        public IEnumerable<Movie> GetMovies()
        {
            try
            {
                IEnumerable<Movie> movieList = null;

                var jsonString = _httpClient.GetAsync(_movieAddresses["GetMoviesAddress"]).Result;
                movieList = _httpClient.DeserializeObject<IEnumerable<Movie>>(jsonString);

                return movieList;
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException($"Error getting movies. {ex.Message}", ex);
            }
        }

        public IEnumerable<Movie> GetMoviesBySeriesId(int seriesId)
        {
            try
            {
                if (seriesId <= 0)
                    throw new ArgumentOutOfRangeException("SeriesId cannot be less than 1");

                IEnumerable<Movie> movieList = null;

                var jsonString = _httpClient.GetAsync($"{_movieAddresses["GetMoviesBySeriesIdAddress"]}/{seriesId}").Result;
                movieList = _httpClient.DeserializeObject<IEnumerable<Movie>>(jsonString);

                return movieList;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting movie. {ex.Message}", ex);
            }
        }

        public bool SaveMovie(Movie movie)
        {
            try
            {
                if (movie == null)
                    throw new ArgumentNullException(nameof(movie));

                bool success = false;

                var jsonString = _httpClient.PostAsync(movie, _movieAddresses["SaveMovieAddress"]).Result;
                success = _httpClient.DeserializeObject<bool>(jsonString);

                return success;
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException($"Error saving movie. {ex.Message}", ex);
            }
        }
    }
}