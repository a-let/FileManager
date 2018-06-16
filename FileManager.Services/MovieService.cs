﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

using FileManager.BusinessLayer;
using FileManager.Services.Interfaces;
using System.Text;

namespace FileManager.Services
{
    public class MovieService : IMovieService
    {
        private readonly string GetMoviesAddress = "api/Movie";
        private readonly string SaveMovieAddress = "api/Movie";

        private readonly IConfiguration _configuration;

        public MovieService(IConfiguration configuration)
        {
            _configuration = configuration;
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

                    var response = await client.GetAsync(GetMoviesAddress);
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
                    var response = await client.PostAsync(SaveMovieAddress, content);

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