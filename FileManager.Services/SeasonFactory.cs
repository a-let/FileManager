using FileManager.Models;
using FileManager.Services.Interfaces;

using Logging;

using Microsoft.Extensions.Configuration;

using System.Net.Http;

namespace FileManager.Services
{
    internal class SeasonFactory : FileManagerFactory<Season>
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger _logger;

        public SeasonFactory(IConfiguration configuration, IHttpClientFactory httpClientFactory, ILogger logger)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public override IService<Season> Create() => new SeasonService(_configuration, _httpClientFactory, _logger);
    }
}