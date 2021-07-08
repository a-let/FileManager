using FileManager.Interfaces;
using FileManager.Models;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using System.Net.Http;


namespace FileManager.Services.Factories
{
    internal class SeriesFactory : FileManagerFactory<Series>
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<SeriesService> _logger;

        public SeriesFactory(IConfiguration configuration, IHttpClientFactory httpClientFactory, ILoggerFactory loggerFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _logger = loggerFactory.CreateLogger<SeriesService>();
        }

        public override IService<Series> Create() => new SeriesService(_configuration, _httpClientFactory, _logger);
    }
}