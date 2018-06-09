using System.Collections.Generic;

using FileManager.BusinessLayer.Interfaces;

namespace FileManager.BusinessLayer
{
    public class Series : FileManagerObjectBase
    {
        public int SeriesId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }

        internal Series() { }

        public static Series NewSeries() => new Series();

        //public void Save()
        //{
        //    _commandText = "dbo.SeriesSave";
        //    var fileManagerDb = GetDb();
        //    using (var connection = fileManagerDb.CreateConnection())
        //    using (var command = fileManagerDb.CreateCommand())
        //    {
        //        connection.Open();
        //        command.Parameters.AddWithValue("@SeriesId", this.SeriesId);
        //        command.Parameters.AddWithValue("@MovieName", this.Name);
        //        command.Parameters.AddWithValue("@Path", this.Path);

        //        command.ExecuteNonQuery();
        //    }
        //}

        //public static IEnumerable<Series> GetSeries()
        //{
        //    var series = new List<Series>();

        //    _commandText = "dbo.SeriesGetList";
        //    var fileManagerDb = GetDb();
        //    using (var connection = fileManagerDb.CreateConnection())
        //    using (var command = fileManagerDb.CreateCommand())
        //    {
        //        connection.Open();
        //        var reader = command.ExecuteReader();

        //        while(reader.Read())
        //        {
        //            series.Add(new Series
        //            {
        //                SeriesId = (int)reader["SeriesId"],
        //                Name = (string)reader["SeriesName"],
        //                Path = (string)reader["FilePath"]
        //            });
        //        }               
        //    }

        //    return series;
        //}

        //public static Series GetSeries(string name)
        //{
        //    var series = new Series();

        //    using (var context = new FileManagerContext())
        //    {
        //        series = context.Series
        //            .SingleOrDefault(s => s.Name.Equals(name, System.StringComparison.OrdinalIgnoreCase));
        //    }

        //    return series;
        //}
    }
}