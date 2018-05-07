using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

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
            using (var connection = _fileManagerDb.CreateConnection())
            using (var command = _fileManagerDb.CreateCommand(_commandText))
            {
                connection.Open();

                command.Parameters.Add(new SqlParameter("@EpisodeId", this.EpisodeId));
                command.Parameters.Add(new SqlParameter("@SeasonId", this.SeasonId));
                command.Parameters.Add(new SqlParameter("@EpisodeName", this.Name));
                command.Parameters.Add(new SqlParameter("@EpisodeNumber", this.EpisodeNumber));
                command.Parameters.Add(new SqlParameter("@EpisodeFormat", this.Format));
                command.Parameters.Add(new SqlParameter("@Path", this.Path));
                command.ExecuteNonQuery();
            }
        }

        public static IEnumerable<Episode> GetEpisodes()
        {            
            var episodes = new List<Episode>();

            _commandText = "dbo.EpisodeGetList";
            using (var connection = _fileManagerDb.CreateConnection())
            using (var command = _fileManagerDb.CreateCommand(_commandText))
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
            using (var connection = _fileManagerDb.CreateConnection())
            using (var command = _fileManagerDb.CreateCommand(_commandText))
            {
                connection.Open();

                command.Parameters.Add(new SqlParameter("@EpisodeId", id));

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
            using (var connection = _fileManagerDb.CreateConnection())
            using (var command = _fileManagerDb.CreateCommand(_commandText))
            {
                connection.Open();

                command.Parameters.Add(new SqlParameter("@EpisodeName", name));

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
            using (var connection = _fileManagerDb.CreateConnection())
            using (var command = _fileManagerDb.CreateCommand(_commandText))
            {
                connection.Open();

                command.Parameters.Add(new SqlParameter("@SeasonId", id));

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