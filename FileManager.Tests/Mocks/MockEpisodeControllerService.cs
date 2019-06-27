﻿using FileManager.Models;
using FileManager.Web.Services.Interfaces;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Tests.Mocks
{
    public class MockEpisodeControllerService : IEpisodeControllerService
    {
        public async Task<Episode> GetEpisodeByIdAsync(int id) => await Task.FromResult(id != 1 ? null : new Episode
        {
            EpisodeId = 1
        });

        public Episode GetEpisodeByName(string name) => string.IsNullOrWhiteSpace(name) ? null : new Episode
        {
            Name = "Test Episode"
        };

        public IEnumerable<Episode> GetEpisodes() => new List<Episode>();

        public IEnumerable<Episode> GetEpisodesBySeasonId(int seasonId) => new List<Episode>();

        public async Task<int> SaveEpisodeAsync(Episode episode) => await Task.FromResult(episode != null ? 1 : 0);
    }
}