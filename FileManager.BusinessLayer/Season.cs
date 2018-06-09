using System.Collections.Generic;

using FileManager.BusinessLayer.Interfaces;

namespace FileManager.BusinessLayer
{
    public class Season : FileManagerObjectBase
    {
        public int SeasonId { get; set; }
        public int ShowId { get; set; }
        public int SeasonNumber { get; set; }
        public IEnumerable<Episode> EpisodeList { get; set; }
        public string Path { get; set; }

        internal Season() { }

        public static Season NewSeason() => new Season();

        //public void Save()
        //{
        //    _commandText = "dbo.SeasonSave";
        //    var fileManagerDb = GetDb();
        //    using (var connection = fileManagerDb.CreateConnection())
        //    using (var command = fileManagerDb.CreateCommand())
        //    {
        //        connection.Open();
        //        command.Parameters.AddWithValue("@SeasonId", this.SeasonId);
        //        command.Parameters.AddWithValue("@ShowId", this.ShowId);
        //        command.Parameters.AddWithValue("@SeasonNumber", this.SeasonNumber);
        //        command.Parameters.AddWithValue("@Path", this.Path);

        //        command.ExecuteNonQuery();
        //    }
        //}

        //public static IEnumerable<Season> GetSeasons()
        //{
        //    var seasons = new List<Season>();

        //    _commandText = "dbo.SeasonGetList";
        //    var fileManagerDb = GetDb();
        //    using (var connection = fileManagerDb.CreateConnection())
        //    using (var command = fileManagerDb.CreateCommand())
        //    {
        //        connection.Open();
        //        var reader = command.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            seasons.Add(new Season
        //            {
        //                SeasonId = (int)reader["SeasonId"],
        //                ShowId = (int)reader["ShowId"],
        //                SeasonNumber = (int)reader["SeasonNumber"],
        //                EpisodeList = Episode.GetEpisodesBySeasonId((int)reader["SeasonId"]),
        //                Path = (string)reader["FilePath"]
        //            });
        //        }
        //    }

        //    return seasons;
        //}

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