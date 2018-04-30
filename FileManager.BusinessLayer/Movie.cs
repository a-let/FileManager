using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using FileManager.BusinessLayer.Interfaces;

namespace FileManager.BusinessLayer
{
    public class Movie : IFileManagerObject, IVideo
    {
        public int MovieId { get; set; }
        public int SeriesId { get; set; }
        public string Name { get; set; }
        public bool IsSeries { get; set; }
        public string Format { get; set; }
        public string Category { get; set; }
        public string Path { get; set; }

        internal Movie() { }

        public static Movie NewMovie() => new Movie();

        public void Save()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FileManager"].ConnectionString))
            using (var command = new SqlCommand("dbo.MovieSave", connection) { CommandType = CommandType.StoredProcedure })
            {
                connection.Open();

                command.Parameters.Add(new SqlParameter("@MovieId", this.MovieId));
                command.Parameters.Add(new SqlParameter("@SeriesId", this.SeriesId));
                command.Parameters.Add(new SqlParameter("@MovieName", this.Name));
                command.Parameters.Add(new SqlParameter("@IsSeries", this.IsSeries));
                command.Parameters.Add(new SqlParameter("@MovieFormat", this.Format));
                command.Parameters.Add(new SqlParameter("@MovieCategory", this.Category));
                command.Parameters.Add(new SqlParameter("@Path", this.Path));
                command.ExecuteNonQuery();
            }
        }

        public static IEnumerable<Movie> GetMovies()
        {
            var movies = new List<Movie>();

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FileManager"].ConnectionString))
            using (var command = new SqlCommand("dbo.MovieGetList", connection) { CommandType = CommandType.StoredProcedure })
            {
                connection.Open();

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    movies.Add(new Movie
                    {
                        MovieId = (int)reader["MovieId"],
                        SeriesId = (int)reader["SeriesId"],
                        Name = (string)reader["MovieName"],
                        IsSeries = (bool)reader["IsSeries"],
                        Format = (string)reader["MovieFormat"],
                        Category = (string)reader["MovieCategory"],
                        Path = (string)reader["FilePath"]
                    });
                }
            }

            return movies;
        }

        //public static Movie GetMovie(string name)
        //{
        //    var movie = new Movie();

        //    using (var context = new FileManagerContext())
        //    {
        //        movie = context.Movie
        //            .SingleOrDefault(e => e.Name.Equals(name, System.StringComparison.OrdinalIgnoreCase));
        //    }

        //    return movie;
        //}

        //public static IEnumerable<Movie> GetMoviesBySeriesId(int id)
        //{
        //    var movies = new List<Movie>();

        //    using (var context = new FileManagerContext())
        //    {
        //        foreach (var m in context.Movie.Where(m => m.IsSeries && m.SeriesId == id))
        //        {
        //            movies.Add(new Movie()
        //            {
        //                MovieId = m.MovieId,
        //                SeriesId = m.SeriesId,
        //                Name = m.Name,
        //                IsSeries = m.IsSeries,
        //                Format = m.Format,
        //                Category = m.Category,
        //                Path = m.Path
        //            });
        //        }                
        //    }

        //    return movies;
        //}
    }
}