using System;
using Microsoft.Extensions.DependencyInjection;
using FileManager.Services.Interfaces;
using FileManager.BusinessLayer;
using Formats = FileManager.BusinessLayer.Helpers.FileFormats.FileFormatTypes;

namespace FileManager.ConsoleApp
{
    public  class Program
    {
        private static IEpisodeService _episodeService;
        private static IMovieService _movieService;
        private static ISeasonService _seasonService;
        private static ISeriesService _seriesService;
        private static IShowService _showService;

        public static void Main(string[] args)
        {
            var services = Setup.CreateServices();
            _episodeService = services.GetService<IEpisodeService>();
            _movieService = services.GetService<IMovieService>();
            _seasonService = services.GetService<ISeasonService>();
            _seriesService = services.GetService<ISeriesService>();
            _showService = services.GetService<IShowService>();

            ShowTest();

            Console.Read();

            services.Dispose();
        }

        private static void EpisodeTest()
        {
            var episodeList = _episodeService.GetEpisodesAsync().Result;
            var episodeById = _episodeService.GetEpisodeByIdAsync(1002).Result;
            var episodeByName = _episodeService.GetEpisodeByNameAsync("episode - from program").Result;

            var newEpisode = Episode.NewEpisode();
            newEpisode.EpisodeId = 0;
            newEpisode.SeasonId = 1002;
            newEpisode.Name = "New episode from console via service";
            newEpisode.EpisodeNumber = 1;
            newEpisode.Format = Formats.mp4.ToString();
            newEpisode.Path = @"C:\Temp";

            var episodeSaved = _episodeService.SaveEpisodeAsync(newEpisode).Result;
            var episodesBySeasonId = _episodeService.GetEpisodeBySeasonIdAsync(1002).Result;

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
            var movieList = _movieService.GetMoviesAsync().Result;

            var newMovie = Movie.NewMovie();
            newMovie.MovieId = 0;
            newMovie.SeriesId = 2;
            newMovie.Name = "New movie from console via service";
            newMovie.IsSeries = false;
            newMovie.Format = Formats.mp4.ToString();
            newMovie.Category = "Testing";
            newMovie.Path = @"C:\Temp";

            var movieSaved = _movieService.SaveMovieAsync(newMovie).Result;

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
            var newSeason = Season.NewSeason();
            newSeason.SeasonId = 0;
            newSeason.ShowId = 1;
            newSeason.SeasonNumber = 1;
            newSeason.Path = "New season path from console via service";

            var seasonSaved = _seasonService.SaveSeasonAsync(newSeason).Result;

            Console.WriteLine("Saved...");
            Console.WriteLine($"{seasonSaved}");

            Console.WriteLine("Get seasons...");
            foreach (var season in _seasonService.GetSeasonsAsync().Result)
            {
                Console.WriteLine($"{season.SeasonId} - {season.Path}");
            }
        }

        private static void SeriesTest()
        {
            var newSeries = Series.NewSeries();
            newSeries.SeriesId = 0;
            newSeries.Name = "New series path from console via service";
            newSeries.Path = @"C:\Temp";

            var seriesSave = _seriesService.SaveSeriesAsync(newSeries).Result;

            Console.WriteLine("Saved...");
            Console.WriteLine($"{seriesSave}");

            Console.WriteLine("Get seasons...");
            foreach (var series in _seriesService.GetSeriesAsync().Result)
            {
                Console.WriteLine($"{series.SeriesId} - {series.Name}");
            }
        }

        private static void ShowTest()
        {
            var newShow = Show.NewShow();
            newShow.ShowId = 0;
            newShow.Name = "New show from console via service";
            newShow.Category = "Testing";
            newShow.Path = @"C:\Temp";

            var showSaved = _showService.SaveShowAsync(newShow).Result;

            Console.WriteLine("Saved...");
            Console.WriteLine($"{showSaved}");

            Console.WriteLine("Get seasons...");
            foreach (var show in _showService.GetShowsAsync().Result)
            {
                Console.WriteLine($"{show.ShowId} - {show.Name}");
            }
        }
    }
}