using System;
using Microsoft.Extensions.DependencyInjection;
using FileManager.Services.Interfaces;
using FileManager.Models;
using Formats = FileManager.Models.Helpers.FileFormats.FileFormatTypes;

namespace FileManager.ConsoleApp
{
    public  class Program
    {
        private static readonly ServiceProvider _services = Setup.CreateServices();

        public static void Main(string[] args)
        {
            EpisodeTest();
            MovieTest();
            SeasonTest();
            SeriesTest();
            ShowTest();

            Console.Read();

            _services.Dispose();
        }

        private static void EpisodeTest()
        {
            var episodeService = _services.GetService<IEpisodeService>();
                        
            var episodeById = episodeService.GetEpisodeById(1002);
            var episodeByName = episodeService.GetEpisodeByName("episode - from program");

            var newEpisode = new Episode
            {
                EpisodeId = 0,
                SeasonId = 1002,
                Name = "New episode from console via service",
                EpisodeNumber = 1,
                Format = Formats.mp4.ToString(),
                Path = @"C:\Temp"
            };    
            
            var episodeSaved = episodeService.SaveEpisode(newEpisode);
            var episodesBySeasonId = episodeService.GetEpisodesBySeasonId(1002);
            
            Console.WriteLine("Get by id...");
            Console.WriteLine($"{episodeById.EpisodeId} - {episodeById.Name}");
            Console.WriteLine("Get by name...");
            Console.WriteLine($"{episodeByName.EpisodeId} - {episodeByName.Name}");
            Console.WriteLine("Saved...");
            Console.WriteLine($"{episodeSaved}");

            var episodeList = episodeService.GetEpisodes();

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
                Name = "New movie from console via service",
                IsSeries = false,
                Format = Formats.mp4.ToString(),
                Category = "Testing",
                Path = @"C:\Temp"
            };
          
            var movieSaved = movieService.SaveMovie(newMovie);
            var movieList = movieService.GetMovies();
            var movieById = movieService.GetMovieById(1007);
            var movieByName = movieService.GetMovieByName("New movie from console via service");
            var moviesBySeriesId = movieService.GetMoviesBySeriesId(1);

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
                Path = "New season path from console via service"
            };

            var seasonSaved = seasonService.SaveSeason(newSeason);
            var seasonById = seasonService.GetSeasonById(1002);
            var seasonByShowId = seasonService.GetSeasonsByShowId(1);

            Console.WriteLine("Saved...");
            Console.WriteLine($"{seasonSaved}");

            Console.WriteLine("Get by id...");
            Console.WriteLine($"{seasonById.SeasonId} - {seasonById.Path}");

            Console.WriteLine("Get seasons...");
            foreach (var season in seasonService.GetSeasons())
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
                Name = "New series path from console via service",
                Path = @"C:\Temp"
            };

            var seriesSave = seriesService.SaveSeriesAsync(newSeries).Result;
            var seriesById = seriesService.GetSeriesByIdAsync(1002).Result;
            var seriesByName = seriesService.GetSeriesByName("Test Series from swagger").Result;

            Console.WriteLine("Saved...");
            Console.WriteLine($"{seriesSave}");

            Console.WriteLine("Get by id...");
            Console.WriteLine($"{seriesById.SeriesId} - {seriesById.Name}");
            Console.WriteLine("Get by name...");
            Console.WriteLine($"{seriesByName.SeriesId} - {seriesByName.Name}");

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
            var showById = showService.GetShowByIdAsync(1).Result;
            var showByName = showService.GetShowByNameAsync("Testing Show - from program").Result;

            Console.WriteLine("Get by id...");
            Console.WriteLine($"{showById.ShowId} - {showById.Name}");
            Console.WriteLine("Get by name...");
            Console.WriteLine($"{showByName.ShowId} - {showByName.Name}");

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