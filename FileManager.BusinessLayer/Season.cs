using System.Collections.Generic;

using FileManager.BusinessLayer.Interfaces;

namespace FileManager.BusinessLayer
{
    public class Season : FileManagerObjectBase, IFileManagerObject
    {
        public int SeasonId { get; set; }
        public int ShowId { get; set; }
        public int SeasonNumber { get; set; }
        public IEnumerable<Episode> EpisodeList { get; set; }
        public string Path { get; set; }

        internal Season() { }

        public static Season NewSeason() => new Season();

        public void Save()
        {
            _commandText = "dbo.SeasonSave";
            using (var connection = _fileManagerDb.CreateConnection())
            using (var command = _fileManagerDb.CreateCommand(_commandText))
            {
                connection.Open();

                _paramDict = new Dictionary<string, object>
                {
                    { "@SeasonId", this.SeasonId },
                    { "@ShowId", this.ShowId },
                    { "@SeasonNumber", this.SeasonNumber },
                    { "@Path", this.Path }
                };

                _fileManagerDb.AddParameters(_paramDict);

                command.ExecuteNonQuery();
            }
        }

        public static IEnumerable<Season> GetSeasons()
        {
            var seasons = new List<Season>();

            _commandText = "dbo.SeasonGetList";
            using (var connection = _fileManagerDb.CreateConnection())
            using (var command = _fileManagerDb.CreateCommand(_commandText))
            {
                connection.Open();

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    seasons.Add(new Season
                    {
                        SeasonId = (int)reader["SeasonId"],
                        ShowId = (int)reader["ShowId"],
                        SeasonNumber = (int)reader["SeasonNumber"],
                        EpisodeList = Episode.GetEpisodesBySeasonId((int)reader["SeasonId"]),
                        Path = (string)reader["FilePath"]
                    });
                }
            }

            return seasons;
        }

        //public static Season GetSeason(int seasonId)
        //{
        //    var season = new Season();

        //    using (var context = new FileManagerContext())
        //    {
        //        season = context.Season.Find(seasonId);
        //    }

        //    return season;
        //}
    }
}