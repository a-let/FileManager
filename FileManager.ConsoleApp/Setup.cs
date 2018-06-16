﻿using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FileManager.Services;
using FileManager.Services.Interfaces;

namespace FileManager.ConsoleApp
{
    public class Setup
    {
        public static ServiceProvider CreateServices()
        {
            var services = new ServiceCollection()
                .AddSingleton<IConfiguration, ConfigurationRoot>(configuration => (ConfigurationRoot)GetConfiguration())
                .AddSingleton<IEpisodeService, EpisodeService>()
                .AddSingleton<IMovieService, MovieService>()
                .AddSingleton<ISeasonService, SeasonService>()
                .AddSingleton<ISeriesService, SeriesService>()
                .AddSingleton<IShowService, ShowService>()
                .BuildServiceProvider();

            return services;
        }

        private static IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }
    }
}