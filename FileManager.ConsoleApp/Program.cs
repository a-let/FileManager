using FileManager.Services.Interfaces;
using FileManager.Models;
using FileManager.Models.Constants;

using Microsoft.Extensions.DependencyInjection;

using System;
using FileManager.Services;
using Microsoft.Extensions.Configuration;

namespace FileManager.ConsoleApp
{
    public  class Program
    {
        private static readonly ServiceProvider _services = Setup.CreateServices();

        public static void Main(string[] args)
        {
            var logger = _services.GetService<Logging.ILogger>();
            var httpFact = _services.GetService<System.Net.Http.IHttpClientFactory>();
            var congif = _services.GetService<IConfiguration>();

            //var episodeService = new FileManager<Episode>(congif, httpFact, logger).CreateService();
            //var movieService = new FileManager<Movie>(congif, httpFact, logger).CreateService();

            var factory = new FileManager.Services.FIleManagerClient(congif, httpFact, logger);
            var episodeService = factory.EpisodeService;
            var movieService = factory.MovieService;

            var episodes = episodeService.GetAsync().Result;

            //EpisodeTest();
            //MovieTest();
            //SeasonTest();
            //SeriesTest();
            //ShowTest();

            Console.Read();

            _services.Dispose();
        }

        private static void EpisodeTest()
        {
            var episodeService = _services.GetService<IEpisodeService>();
                        
            var episodeById = episodeService.GetAsync(2).Result;
            var episodeByName = episodeService.GetAsync("string").Result;

            var newEpisode = new Episode
            {
                EpisodeId = 0,
                SeasonId = 2,
                Name = $"New episode from console via service - {DateTime.Now.ToShortDateString()}",
                EpisodeNumber = 1,
                Format = FileFormatTypes.MP4,
                Path = @"C:\Temp"
            };    
            
            var episodeSaved = episodeService.SaveAsync(newEpisode).Result;
            var episodesBySeasonId = episodeService.GetEpisodesBySeasonId(1).Result;
            
            Console.WriteLine("Get by id...");
            Console.WriteLine($"{episodeById.EpisodeId} - {episodeById.Name}");
            Console.WriteLine("Get by name...");
            Console.WriteLine($"{episodeByName.EpisodeId} - {episodeByName.Name}");
            Console.WriteLine("Saved...");
            Console.WriteLine($"{episodeSaved}");

            var episodeList = episodeService.GetAsync().Result;

            Console.WriteLine("Get episodes...");
            foreach (var episode in episodeList)
            {
                Console.WriteLine($"{episode.EpisodeId} - {episode.Name}");
            }

            Console.WriteLine("Get by season id...");
            foreach (var episode in episodesBySeasonId)
            {
                Console.WriteLine($"{episode.EpisodeId} - {episode.Name}");
            }
        }

        private static void MovieTest()
        {
            var movieService = _services.GetService<IMovieService>();    

            var newMovie = new Movie
            {
                MovieId = 0,
                SeriesId = 2,
                Name = $"New movie from console via service - {DateTime.Now.ToShortDateString()}",
                IsSeries = false,
                Format = FileFormatTypes.MP4,
                Category = "Testing",
                Path = @"C:\Temp"
            };
          
            var movieSaved = movieService.SaveAsync(newMovie).Result;
            var movieList = movieService.GetAsync().Result;
            var movieById = movieService.GetAsync(1).Result;
            var movieByName = movieService.GetAsync("string").Result;
            var moviesBySeriesId = movieService.GetMoviesBySeriesId(1).Result;

            Console.WriteLine("Saved...");
            Console.WriteLine($"{movieSaved}");

            Console.WriteLine("Get movies...");
            foreach (var movie in movieList)
            {
                Console.WriteLine($"{movie.MovieId} - {movie.Name}");
            }

            Console.WriteLine("Get by id...");
            Console.WriteLine($"{movieById.MovieId} - {movieById.Name}");
            Console.WriteLine("Get by name...");
            Console.WriteLine($"{movieByName.MovieId} - {movieByName.Name}");

            Console.WriteLine("Get by series id...");
            foreach (var movie in moviesBySeriesId)
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
                Path = $"New season path from console via service - {DateTime.Now.ToShortDateString()}"
            };

            var seasonSaved = seasonService.SaveAsync(newSeason).Result;
            var seasonById = seasonService.GetAsync(2).Result;
            var seasonByShowId = seasonService.GetSeasonsByShowId(2).Result;

            Console.WriteLine("Saved...");
            Console.WriteLine($"{seasonSaved}");

            Console.WriteLine("Get by id...");
            Console.WriteLine($"{seasonById.SeasonId} - {seasonById.Path}");

            Console.WriteLine("Get seasons...");
            foreach (var season in seasonService.GetAsync().Result)
            {
                Console.WriteLine($"{season.SeasonId} - {season.Path}");
            }

            Console.WriteLine("Get seasons by show id...");
            foreach (var season in seasonByShowId)
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
                Name = $"New series path from console via service - {DateTime.Now.ToShortDateString()}",
                Path = @"C:\Temp"
            };

            var seriesSave = seriesService.SaveAsync(newSeries).Result;
            var seriesById = seriesService.GetAsync(1).Result;
            var seriesByName = seriesService.GetAsync("Test Series").Result;

            Console.WriteLine("Saved...");
            Console.WriteLine($"{seriesSave}");

            Console.WriteLine("Get by id...");
            Console.WriteLine($"{seriesById.SeriesId} - {seriesById.Name}");
            Console.WriteLine("Get by name...");
            Console.WriteLine($"{seriesByName.SeriesId} - {seriesByName.Name}");

            Console.WriteLine("Get series...");
            foreach (var series in seriesService.GetAsync().Result)
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
                Name = $"New show from console via service - {DateTime.Now.ToShortDateString()}",
                Category = "Testing",
                Path = @"C:\Temp"
            };

            var showSaved = showService.SaveAsync(newShow).Result;
            var showById = showService.GetAsync(1).Result;
            var showByName = showService.GetAsync("Test Show").Result;

            Console.WriteLine("Get by id...");
            Console.WriteLine($"{showById.ShowId} - {showById.Name}");
            Console.WriteLine("Get by name...");
            Console.WriteLine($"{showByName.ShowId} - {showByName.Name}");

            Console.WriteLine("Saved...");
            Console.WriteLine($"{showSaved}");

            Console.WriteLine("Get shows...");
            foreach (var show in showService.GetAsync().Result)
            {
                Console.WriteLine($"{show.ShowId} - {show.Name}");
            }
        }
    }
}