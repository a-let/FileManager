﻿using System;
using System.Collections.Generic;
using FileManager.BusinessLayer.Interfaces;

namespace FileManager.BusinessLayer.Adapters
{
    public class SeasonAdapter : IFileManagerObjectAdapter<Season>
    {
        private readonly IFileManagerDb _fileManagerDb;
        private readonly IFileManagerObjectAdapter<Episode> _episodeAdapter;

        public SeasonAdapter(IFileManagerDb fileManagerDb, IFileManagerObjectAdapter<Episode> episodeAdapter)
        {
            _fileManagerDb = fileManagerDb;
            _episodeAdapter = episodeAdapter;
        }

        public IEnumerable<Season> Get()
        {
            var seasons = new List<Season>();

            using (var connection = _fileManagerDb.CreateConnection())
            using (var command = _fileManagerDb.CreateCommand())
            {
                connection.Open();
                command.CommandText = "dbo.SeasonGetList";
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    seasons.Add(new Season
                    {
                        SeasonId = (int)reader["SeasonId"],
                        ShowId = (int)reader["ShowId"],
                        SeasonNumber = (int)reader["SeasonNumber"],
                        //EpisodeList = Episode.GetEpisodesBySeasonId((int)reader["SeasonId"]),
                        EpisodeList = _episodeAdapter.GetByParentId((int)reader["SeasonId"]),
                        Path = (string)reader["FilePath"]
                    });
                }
            }

            return seasons;
        }

        public Season GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Season GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Season> GetByParentId(int parentId)
        {
            throw new NotImplementedException();
        }

        public bool Save(Season target)
        {
            try
            {
                using (var connection = _fileManagerDb.CreateConnection())
                using (var command = _fileManagerDb.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = "dbo.SeasonSave";
                    command.Parameters.AddWithValue("@SeasonId", target.SeasonId);
                    command.Parameters.AddWithValue("@ShowId", target.ShowId);
                    command.Parameters.AddWithValue("@SeasonNumber", target.SeasonNumber);
                    command.Parameters.AddWithValue("@Path", target.Path);

                    command.ExecuteNonQuery();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}