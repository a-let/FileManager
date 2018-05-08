using System.Collections.Generic;

using FileManager.BusinessLayer.Interfaces;

namespace FileManager.BusinessLayer
{
    public class Episode : FileManagerObjectBase, IFileManagerObject, IVideo
    {
        public int EpisodeId { get; set; }
        public int SeasonId { get; set; }
        public string Name { get; set; }

        private int _episodeNumber;
        public int EpisodeNumber
        {
            get => _episodeNumber;
            set
            {
                if (value != _episodeNumber)
                {
                    SetProperty("EpisodeNumber", ref _episodeNumber, value);
                    _episodeNumber = value;
                }
            }
        }

        private string _format;
        public string Format
        {
            get => _format;
            set
            {
                if(value != _format)
                {
                    SetProperty("Format", ref _format, value);
                    _format = value;                    
                }
            }
        }

        private string _path;
        public string Path
        {
            get => _path;
            set
            {
                if (value != _path)
                {
                    SetProperty("Path", ref _path, value);
                    _path = value;
                }
            }
        }
                
        internal Episode() { }

        public static Episode NewEpisode() => new Episode();

        public void Save()
        {
            _commandText = "dbo.EpisodeSave";
            var fileManagerDb = GetDb();
            using (var connection = fileManagerDb.CreateConnection())
            using (var command = fileManagerDb.CreateCommand())
            {
                connection.Open();
                command.Parameters.AddWithValue("@EpisodeId", this.EpisodeId);
                command.Parameters.AddWithValue("@SeasonId", this.SeasonId);
                command.Parameters.AddWithValue("@EpisodeName", this.Name);
                command.Parameters.AddWithValue("@EpisodeNumber", this.EpisodeNumber);
                command.Parameters.AddWithValue("@EpisodeFormat", this.Format);
                command.Parameters.AddWithValue("@Path", this.Path);

                command.ExecuteNonQuery();
            }
        }

        public static IEnumerable<Episode> GetEpisodes()
        {            
            var episodes = new List<Episode>();

            _commandText = "dbo.EpisodeGetList";
            var fileManagerDb = GetDb();
            using (var connection = fileManagerDb.CreateConnection())
            using (var command = fileManagerDb.CreateCommand())
            {
                connection.Open();
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

        public static Episode GetEpisode(int id)
        {            
            var episode = new Episode();

            _commandText = "dbo.EpisodeGetById";
            var fileManagerDb = GetDb();
            using (var connection = fileManagerDb.CreateConnection())
            using (var command = fileManagerDb.CreateCommand())
            {
                connection.Open();
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

        public static Episode GetEpisode(string name)
        {
            
            var episode = new Episode();

            _commandText = "dbo.EpisodeGetByName";
            var fileManagerDb = GetDb();
            using (var connection = fileManagerDb.CreateConnection())
            using (var command = fileManagerDb.CreateCommand())
            {
                connection.Open();
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

        public static IEnumerable<Episode> GetEpisodesBySeasonId(int id)
        {            
            var episodes = new List<Episode>();

            _commandText = "dbo.EpisodeGetBySeasonId";
            var fileManagerDb = GetDb();
            using (var connection = fileManagerDb.CreateConnection())
            using (var command = fileManagerDb.CreateCommand())
            {
                connection.Open();
                command.Parameters.AddWithValue("@SeasonId", id);

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