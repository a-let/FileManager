using System.Collections.Generic;
using FileManager.BusinessLayer.Interfaces;

namespace FileManager.BusinessLayer
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
            var episode = new Episode();

            using (var connection = _fileManagerDb.CreateConnection())
            using (var command = _fileManagerDb.CreateCommand())
            {
                connection.Open();
                command.CommandText = "dbo.EpisodeGetById";
                command.Parameters.AddWithValue("@EpisodeId", id);

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
            var episode = new Episode();

            using (var connection = _fileManagerDb.CreateConnection())
            using (var command = _fileManagerDb.CreateCommand())
            {
                connection.Open();
                command.CommandText = "dbo.EpisodeGetByName";
                command.Parameters.AddWithValue("@EpisodeName", name);

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
                    command.Parameters.AddWithValue("@EpisodeId", target.EpisodeId);
                    command.Parameters.AddWithValue("@SeasonId", target.SeasonId);
                    command.Parameters.AddWithValue("@EpisodeName", target.Name);
                    command.Parameters.AddWithValue("@EpisodeNumber", target.EpisodeNumber);
                    command.Parameters.AddWithValue("@EpisodeFormat", target.Format);
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

        public IEnumerable<Episode> GetByParentId(int parentId)
        {
            var episodes = new List<Episode>();

            using (var connection = _fileManagerDb.CreateConnection())
            using (var command = _fileManagerDb.CreateCommand())
            {
                connection.Open();
                command.CommandText = "dbo.EpisodeGetBySeasonId";
                command.Parameters.AddWithValue("@SeasonId", parentId);

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