using FileManager.Interfaces;
using FileManager.Models;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using System.Net.Http;

namespace FileManager.Services.Factories
{
    internal class ShowFactory : FileManagerFactory<Show>
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ShowService> _logger;

        public ShowFactory(IConfiguration configuration, IHttpClientFactory httpClientFactory, ILoggerFactory loggerFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _logger = loggerFactory.CreateLogger<ShowService>();
        }

        public override IService<Show> Create() => new ShowService(_configuration, _httpClientFactory, _logger);
    }
}