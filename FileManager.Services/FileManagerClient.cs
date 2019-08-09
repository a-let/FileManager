using FileManager.Models;
using FileManager.Services.Interfaces;

using Logging;

using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;


namespace FileManager.Services
{
    public class FIleManagerClient : IFileManagerClient
    {
        private readonly IList<object> _factories;
        private string[] _services = new[] { "Episode", "Movie" };

        public FIleManagerClient(IConfiguration configuration, IHttpClientFactory httpClientFactory, ILogger logger)
        {
            _factories = new List<object>();

            foreach (var service in _services)
            {
                var factory = Activator
                    .CreateInstance(Type.GetType($"FileManager.Services.{service}Factory"), configuration, httpClientFactory, logger);

                _factories.Add(factory);
            }
        }

        public IEpisodeService EpisodeService => (IEpisodeService)CreateService<Episode>();

        public IMovieService MovieService => (IMovieService)CreateService<Movie>();

        private IService<T> CreateService<T>() => ((FileManagerFactory<T>)_factories
            .Single(x => x.GetType() == Type.GetType($"FileManager.Services.{typeof(T).Name}Factory"))).Create();
    }
}