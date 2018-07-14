﻿using System;
using System.Collections.Generic;
using FileManager.Models;
using FileManager.BusinessLayer.Interfaces;

namespace FileManager.Web.Services
{
    public class EpisodeControllerService : IEpisodeControllerService
    {
        private readonly IFileManagerObjectRepository<Episode> _episodeRepository;

        public EpisodeControllerService(IFileManagerObjectRepository<Episode> episodeRepository)
        {
            _episodeRepository = episodeRepository;
        }

        public Episode GetEpisodeById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid EpisodeId");

            return _episodeRepository.GetById(id);
        }

        public IEnumerable<Episode> GetEpisodes()
        {
            return _episodeRepository.Get();
        }

        public Episode GetEpisodeByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException();

            return _episodeRepository.GetByName(name);
        }

        public bool SaveEpisode(Episode episode)
        {
            if (episode == null)
                throw new ArgumentNullException(nameof(episode));

            return _episodeRepository.Save(episode);
        }

        public IEnumerable<Episode> GetEpisodesBySeasonId(int seasonId)
        {
            if (seasonId <= 0)
                throw new ArgumentException("Invalid SeasonId");

            return _episodeRepository.GetByParentId(seasonId);
        }
    }
}