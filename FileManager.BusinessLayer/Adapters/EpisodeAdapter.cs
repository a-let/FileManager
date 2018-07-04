using System.Collections.Generic;

using FileManager.BusinessLayer.Interfaces;
using FileManager.Models;

namespace FileManager.BusinessLayer.Adapters
{
    public class EpisodeAdapter : IFileManagerObjectAdapter<Episode>
    {
        private readonly IFileManagerDb _fileManagerDb;

        public EpisodeAdapter(IFileManagerDb fileManagerDb)
        {
            _fileManagerDb = fileManagerDb;
        }

        public Episode GetById(int id)
        {
            Episode episode = null;

            using (var connection = _fileManagerDb.CreateConnection())
            using (var command = _fileManagerDb.CreateCommand())
            {
                connection.Open();
                command.CommandText = "dbo.EpisodeGetById";
                command.Parameters.Add(_fileManagerDb.CreateParameter("@EpisodeId", id));

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    episode = new Episode
                    {
                        EpisodeId = (int)reader["EpisodeId"],
                        SeasonId = (int)reader["SeasonId"],
                        Name = (string)reader["EpisodeName"],
                        EpisodeNumber = (int)reader["EpisodeNumber"],
                        Format = (string)reader["EpisodeFormat"],
                        Path = (string)reader["FilePath"]
                    };
                }
            }

            return episode;
        }

        public IEnumerable<Episode> Get()
        {
            var episodes = new List<Episode>();

            using (var connection = _fileManagerDb.CreateConnection())
            using (var command = _fileManagerDb.CreateCommand())
            {
                connection.Open();
                command.CommandText = "dbo.EpisodeGetList";
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    episodes.Add(new Episode
                    {
                        EpisodeId = (int)reader["EpisodeId"],
                        SeasonId = (int)reader["SeasonId"],
                        Name = (string)reader["EpisodeName"],
                        EpisodeNumber = (int)reader["EpisodeNumber"],
                        Format = (string)reader["EpisodeFormat"],
                        Path = (string)reader["FilePath"]
                    });
                }
            }

            return episodes;
        }

        public Episode GetByName(string name)
        {
            Episode episode = null;

            using (var connection = _fileManagerDb.CreateConnection())
            using (var command = _fileManagerDb.CreateCommand())
            {
                connection.Open();
                command.CommandText = "dbo.EpisodeGetByName";
                command.Parameters.Add(_fileManagerDb.CreateParameter("@EpisodeName", name));

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    episode = new Episode
                    {
                        EpisodeId = (int)reader["EpisodeId"],
                        SeasonId = (int)reader["SeasonId"],
                        Name = (string)reader["EpisodeName"],
                        EpisodeNumber = (int)reader["EpisodeNumber"],
                        Format = (string)reader["EpisodeFormat"],
                        Path = (string)reader["FilePath"]
                    };
                }
            }

            return episode;
        }

        public bool Save(Episode target)
        {
            try
            {
                using (var connection = _fileManagerDb.CreateConnection())
                using (var command = _fileManagerDb.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = "dbo.EpisodeSave";
                    command.Parameters.Add(_fileManagerDb.CreateParameter("@EpisodeId", target.EpisodeId));
                    command.Parameters.Add(_fileManagerDb.CreateParameter("@SeasonId", target.SeasonId));
                    command.Parameters.Add(_fileManagerDb.CreateParameter("@EpisodeName", target.Name));
                    command.Parameters.Add(_fileManagerDb.CreateParameter("@EpisodeNumber", target.EpisodeNumber));
                    command.Parameters.Add(_fileManagerDb.CreateParameter("@EpisodeFormat", target.Format));
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

        public IEnumerable<Episode> GetByParentId(int parentId)
        {
            var episodes = new List<Episode>();

            using (var connection = _fileManagerDb.CreateConnection())
            using (var command = _fileManagerDb.CreateCommand())
            {
                connection.Open();
                command.CommandText = "dbo.EpisodeGetBySeasonId";
                command.Parameters.Add(_fileManagerDb.CreateParameter("@SeasonId", parentId));

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    episodes.Add(new Episode
                    {
                        EpisodeId = (int)reader["EpisodeId"],
                        SeasonId = (int)reader["SeasonId"],
                        Name = (string)reader["EpisodeName"],
                        EpisodeNumber = (int)reader["EpisodeNumber"],
                        Format = (string)reader["EpisodeFormat"],
                        Path = (string)reader["FilePath"]
                    });
                }
            }

            return episodes;
        }
    }
}