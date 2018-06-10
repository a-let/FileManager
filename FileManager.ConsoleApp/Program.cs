using System;
using Microsoft.Extensions.DependencyInjection;
using FileManager.Services.Interfaces;

namespace FileManager.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = Setup.CreateServices();
            var episodeService = services.GetService<IEpisodeService>();

            var episodeList = episodeService.GetEpisodesAsync().Result;

            foreach(var episode in episodeList)
            {
                Console.WriteLine($"{episode.EpisodeId} - {episode.Name}");
            }

            Console.Read();

            services.Dispose();
        }
    }
}