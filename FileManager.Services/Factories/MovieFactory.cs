using FileManager.Models;
using FileManager.Services.Interfaces;

using Logging;

using Microsoft.Extensions.Configuration;

using System.Net.Http;

namespace FileManager.Services.Factories
{
    internal class MovieFactory : FileManagerFactory<Movie>
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger _logger;

        public MovieFactory(IConfiguration configuration, IHttpClientFactory httpClientFactory, ILogger logger)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public override IService<Movie> Create() => new MovieService(_configuration, _httpClientFactory, _logger);
    }
}