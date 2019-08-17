using FileManager.Models;
using FileManager.Services.Factories;
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
        private string[] _services = new[] 
        {
            nameof(Episode),
            nameof(Movie),
            nameof(Season),
            nameof(Series),
            nameof(Show)
        };

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

        public ISeasonService SeasonService => (ISeasonService)CreateService<Season>();

        public ISeriesService SeriesService => (ISeriesService)CreateService<Series>();

        public IShowService ShowService => (IShowService)CreateService<Show>();

        private IService<T> CreateService<T>() => ((FileManagerFactory<T>)_factories
            .Single(x => x.GetType() == Type.GetType($"FileManager.Services.{typeof(T).Name}Factory"))).Create();
    }
}