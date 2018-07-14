using System;
using System.Collections.Generic;
using System.Data;

using FileManager.BusinessLayer.Interfaces;
using FileManager.Models;

namespace FileManager.BusinessLayer.Repositories
{
    public class SeasonRepository : IFileManagerObjectRepository<Season>
    {
        private readonly IFileManagerDb _fileManagerDb;
        private readonly IFileManagerObjectRepository<Episode> _episodeAdapter;

        public SeasonRepository(IFileManagerDb fileManagerDb, IFileManagerObjectRepository<Episode> episodeAdapter)
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

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        seasons.Add(CreateFromReader(reader));
                    }
                }
            }

            return seasons;
        }

        public Season GetById(int id)
        {
            Season season = null;

            using (var connection = _fileManagerDb.CreateConnection())
            using (var command = _fileManagerDb.CreateCommand())
            {
                connection.Open();
                command.CommandText = "dbo.SeasonGetById";
                command.Parameters.Add(_fileManagerDb.CreateParameter("@SeasonId", id));

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        season = CreateFromReader(reader);
                    }
                }
            }

            return season;
        }

        public Season GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Season> GetByParentId(int parentId)
        {
            var seasons = new List<Season>();

            using (var connection = _fileManagerDb.CreateConnection())
            using (var command = _fileManagerDb.CreateCommand())
            {
                connection.Open();
                command.CommandText = "dbo.SeasonGetByShowId";
                command.Parameters.Add(_fileManagerDb.CreateParameter(@"ShowId", parentId));

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        seasons.Add(CreateFromReader(reader));
                    }
                }

                return seasons;
            }
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
                    command.Parameters.Add(_fileManagerDb.CreateParameter("@SeasonId", target.SeasonId));
                    command.Parameters.Add(_fileManagerDb.CreateParameter("@ShowId", target.ShowId));
                    command.Parameters.Add(_fileManagerDb.CreateParameter("@SeasonNumber", target.SeasonNumber));
                    command.Parameters.Add(_fileManagerDb.CreateParameter("@Path", target.Path));

                    command.ExecuteNonQuery();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        private Season CreateFromReader(IDataReader reader) => new Season
        {
            SeasonId = (int)reader["SeasonId"],
            ShowId = (int)reader["ShowId"],
            SeasonNumber = (int)reader["SeasonNumber"],
            EpisodeList = _episodeAdapter.GetByParentId((int)reader["SeasonId"]),
            Path = (string)reader["FilePath"]
        };
    }
}