using FileManager.Interfaces;
using FileManager.Models;
using FileManager.Services.Factories;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace FileManager.Services
{
    public class FileManagerClient : IFileManagerClient
    {
        private readonly IList<object> _factories;
        private readonly string[] _services = new[] 
        {
            nameof(Episode),
            nameof(Movie),
            nameof(Season),
            nameof(Series),
            nameof(Show)
        };

        public FileManagerClient(IConfiguration configuration, IHttpClientFactory httpClientFactory, ILoggerFactory loggerFactory)
        {
            _factories = new List<object>();

            foreach (var service in _services)
            {
                var factory = Activator
                    .CreateInstance(Type.GetType($"FileManager.Services.Factories.{service}Factory"), configuration, httpClientFactory, loggerFactory);

                _factories.Add(factory);
            }
        }

        public IEpisodeService EpisodeService => (IEpisodeService)CreateService<Episode>();

        public IMovieService MovieService => (IMovieService)CreateService<Movie>();

        public ISeasonService SeasonService => (ISeasonService)CreateService<Season>();

        public ISeriesService SeriesService => (ISeriesService)CreateService<Series>();

        public IShowService ShowService => (IShowService)CreateService<Show>();

        private IService<T> CreateService<T>() => ((FileManagerFactory<T>)_factories
            .Single(x => x.GetType() == Type.GetType($"FileManager.Services.Factories.{typeof(T).Name}Factory"))).Create();
    }
}