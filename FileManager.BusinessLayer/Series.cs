using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using FileManager.BusinessLayer.Interfaces;

namespace FileManager.BusinessLayer
{
    public class Series : IFileManagerObject
    {
        public int SeriesId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }

        internal Series() { }

        public static Series NewSeries() => new Series();

        public void Save()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FileManager"].ConnectionString))
            using (var command = new SqlCommand("dbo.SeriesSave", connection) { CommandType = CommandType.StoredProcedure })
            {
                connection.Open();

                command.Parameters.Add(new SqlParameter("@SeriesId", this.SeriesId));
                command.Parameters.Add(new SqlParameter("@SeriesName", this.Name));
                command.Parameters.Add(new SqlParameter("@Path", this.Path));
                command.ExecuteNonQuery();
            }
        }

        public static IEnumerable<Series> GetSeries()
        {
            var series = new List<Series>();

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FileManager"].ConnectionString))
            using (var command = new SqlCommand("dbo.SeriesGetList", connection) { CommandType = CommandType.StoredProcedure})
            {
                connection.Open();

                var reader = command.ExecuteReader();

                while(reader.Read())
                {
                    series.Add(new Series
                    {
                        SeriesId = (int)reader["SeriesId"],
                        Name = (string)reader["SeriesName"],
                        Path = (string)reader["FilePath"]
                    });
                }               
            }

            return series;
        }

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