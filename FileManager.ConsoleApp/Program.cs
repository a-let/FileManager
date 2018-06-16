﻿using System;
using Microsoft.Extensions.DependencyInjection;
using FileManager.Services.Interfaces;
using FileManager.Models;
using Formats = FileManager.BusinessLayer.Helpers.FileFormats.FileFormatTypes;

namespace FileManager.ConsoleApp
{
    public  class Program
    {
        private static ServiceProvider _services;

        public static void Main(string[] args)
        {
            var services = Setup.CreateServices();

            EpisodeTest();
            MovieTest();
            SeasonTest();
            SeriesTest();
            ShowTest();

            Console.Read();

            services.Dispose();
        }

        private static void EpisodeTest()
        {
            var episodeService = _services.GetService<IEpisodeService>();

            var episodeList = episodeService.GetEpisodesAsync().Result;
            var episodeById = episodeService.GetEpisodeByIdAsync(1002).Result;
            var episodeByName = episodeService.GetEpisodeByNameAsync("episode - from program").Result;

            var newEpisode = new Episode
            {
                EpisodeId = 0,
                SeasonId = 1002,
                Name = "New episode from console via service",
                EpisodeNumber = 1,
                Format = Formats.mp4.ToString(),
                Path = @"C:\Temp"
            };    
            
            var episodeSaved = episodeService.SaveEpisodeAsync(newEpisode).Result;
            var episodesBySeasonId = episodeService.GetEpisodeBySeasonIdAsync(1002).Result;

            Console.WriteLine("Get episodes...");
            foreach (var episode in episodeList)
            {
                Console.WriteLine($"{episode.EpisodeId} - {episode.Name}");
            }

            Console.WriteLine("Get by id...");
            Console.WriteLine($"{episodeById.EpisodeId} - {episodeById.Name}");
            Console.WriteLine("Get by name...");
            Console.WriteLine($"{episodeByName.EpisodeId} - {episodeByName.Name}");
            Console.WriteLine("Saved...");
            Console.WriteLine($"{episodeSaved}");

            Console.WriteLine("Get by season id...");
            foreach (var episode in episodesBySeasonId)
            {
                Console.WriteLine($"{episode.EpisodeId} - {episode.Name}");
            }
        }

        private static void MovieTest()
        {

            var movieService = _services.GetService<IMovieService>();

            var movieList = movieService.GetMoviesAsync().Result;

            var newMovie = new Movie
            {
                MovieId = 0,
                SeriesId = 2,
                Name = "New movie from console via service",
                IsSeries = false,
                Format = Formats.mp4.ToString(),
                Category = "Testing",
                Path = @"C:\Temp"
            };
          
            var movieSaved = movieService.SaveMovieAsync(newMovie).Result;

            Console.WriteLine("Saved...");
            Console.WriteLine($"{movieSaved}");

            Console.WriteLine("Get movies...");
            foreach (var movie in movieList)
            {
                Console.WriteLine($"{movie.MovieId} - {movie.Name}");
            }
        }

        private static void SeasonTest()
        {
            var seasonService = _services.GetService<ISeasonService>();

            var newSeason = new Season
            {
                SeasonId = 0,
                ShowId = 1,
                SeasonNumber = 1,
                Path = "New season path from console via service"
            };

            var seasonSaved = seasonService.SaveSeasonAsync(newSeason).Result;

            Console.WriteLine("Saved...");
            Console.WriteLine($"{seasonSaved}");

            Console.WriteLine("Get seasons...");
            foreach (var season in seasonService.GetSeasonsAsync().Result)
            {
                Console.WriteLine($"{season.SeasonId} - {season.Path}");
            }
        }

        private static void SeriesTest()
        {
            var seriesService = _services.GetService<ISeriesService>();

            var newSeries = new Series
            {
                SeriesId = 0,
                Name = "New series path from console via service",
                Path = @"C:\Temp"
            };

            var seriesSave = seriesService.SaveSeriesAsync(newSeries).Result;

            Console.WriteLine("Saved...");
            Console.WriteLine($"{seriesSave}");

            Console.WriteLine("Get seasons...");
            foreach (var series in seriesService.GetSeriesAsync().Result)
            {
                Console.WriteLine($"{series.SeriesId} - {series.Name}");
            }
        }

        private static void ShowTest()
        {
            var showService = _services.GetService<IShowService>();

            var newShow = new Show
            {
                ShowId = 0,
                Name = "New show from console via service",
                Category = "Testing",
                Path = @"C:\Temp"
            };

            var showSaved = showService.SaveShowAsync(newShow).Result;

            Console.WriteLine("Saved...");
            Console.WriteLine($"{showSaved}");

            Console.WriteLine("Get seasons...");
            foreach (var show in showService.GetShowsAsync().Result)
            {
                Console.WriteLine($"{show.ShowId} - {show.Name}");
            }
        }
    }
}