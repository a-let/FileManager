using FileManager.Interfaces;
using FileManager.Models;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using System.Net.Http;

namespace FileManager.Services.Factories
{
    internal class MovieFactory : FileManagerFactory<Movie>
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<MovieService> _logger;

        public MovieFactory(IConfiguration configuration, IHttpClientFactory httpClientFactory, ILoggerFactory loggerFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _logger = loggerFactory.CreateLogger<MovieService>();
        }

        public override IService<Movie> Create() => new MovieService(_configuration, _httpClientFactory, _logger);
    }
}