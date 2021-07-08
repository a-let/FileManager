using FileManager.Interfaces;
using FileManager.Models;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using System.Net.Http;

namespace FileManager.Services.Factories
{
    internal class EpisodeFactory : FileManagerFactory<Episode>
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<EpisodeService> _logger;

        public EpisodeFactory(IConfiguration configuration, IHttpClientFactory httpClientFactory, ILoggerFactory loggerFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _logger = loggerFactory.CreateLogger<EpisodeService>();
        }

        public override IService<Episode> Create() => new EpisodeService(_configuration, _httpClientFactory, _logger);
    }
}