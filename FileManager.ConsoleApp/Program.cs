using System;
using Microsoft.Extensions.DependencyInjection;
using FileManager.Services.Interfaces;
using FileManager.BusinessLayer;
using Formats = FileManager.BusinessLayer.Helpers.FileFormats.FileFormatTypes;

namespace FileManager.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = Setup.CreateServices();
            var episodeService = services.GetService<IEpisodeService>();

            var episodeList = episodeService.GetEpisodesAsync().Result;
            var episodeById = episodeService.GetEpisodeByIdAsync(1002).Result;
            var episodeByName = episodeService.GetEpisodeByNameAsync("episode - from program").Result;

            var newEpisode = Episode.NewEpisode();
            newEpisode.EpisodeId = 0;
            newEpisode.SeasonId = 1002;
            newEpisode.Name = "New episode from console via service";
            newEpisode.EpisodeNumber = 1;
            newEpisode.Format = Formats.mp4.ToString();
            newEpisode.Path = @"C:\Temp";

            var episodeSaved = episodeService.SaveEpisodeAsync(newEpisode).Result;
            var episodesBySeasonId = episodeService.GetEpisodeBySeasonIdAsync(1002).Result;

            Console.WriteLine("Get episodes...");
            foreach(var episode in episodeList)
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

            Console.Read();

            services.Dispose();
        }
    }
}