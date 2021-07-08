using FileManager.Interfaces;
using FileManager.Models;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using System.Net.Http;

namespace FileManager.Services.Factories
{
    internal class SeasonFactory : FileManagerFactory<Season>
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<SeasonService> _logger;

        public SeasonFactory(IConfiguration configuration, IHttpClientFactory httpClientFactory, ILoggerFactory loggerFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _logger = loggerFactory.CreateLogger<SeasonService>();
        }

        public override IService<Season> Create() => new SeasonService(_configuration, _httpClientFactory, _logger);
    }
}