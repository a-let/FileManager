using FileManager.Services;
using FileManager.Services.Interfaces;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.IO;
using System.Net.Http.Headers;

namespace FileManager.ConsoleApp
{
    public class Setup
    {
        private static IConfiguration _config;

        public static ServiceProvider CreateServices()
        {
            var services = new ServiceCollection()
                .AddSingleton<IConfiguration, ConfigurationRoot>(configuration => (ConfigurationRoot)GetConfiguration())
                .AddSingleton<IEpisodeService, EpisodeService>()
                .AddSingleton<IMovieService, MovieService>()
                .AddSingleton<ISeasonService, SeasonService>()
                .AddSingleton<ISeriesService, SeriesService>()
                .AddSingleton<IShowService, ShowService>();

            services.AddHttpClient("FileManager", c =>
            {
                c.BaseAddress = new Uri(_config["FileManagerBaseAddress"]);
                c.DefaultRequestHeaders.Clear();
                c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });


            return services.BuildServiceProvider();
        }

        private static IConfiguration GetConfiguration()
        {
            _config =  new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            return _config;
        }
    }
}