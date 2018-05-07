using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

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
            using (var connection = _fileManagerDb.CreateConnection())
            using (var command = new SqlCommand("dbo.SeasonSave", connection) { CommandType = CommandType.StoredProcedure })
            {
                connection.Open();

                command.Parameters.Add(new SqlParameter("@SeasonId", this.SeasonId));
                command.Parameters.Add(new SqlParameter("@ShowId", this.ShowId));
                command.Parameters.Add(new SqlParameter("@SeasonNumber", this.SeasonNumber));
                command.Parameters.Add(new SqlParameter("@Path", this.Path));
                command.ExecuteNonQuery();
            }
        }

        public static IEnumerable<Season> GetSeasons()
        {
            var seasons = new List<Season>();

            using (var connection = _fileManagerDb.CreateConnection())
            using (var command = new SqlCommand("dbo.SeasonGetList", connection) { CommandType = CommandType.StoredProcedure })
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