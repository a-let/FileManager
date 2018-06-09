using System.Collections.Generic;

using FileManager.BusinessLayer.Interfaces;

namespace FileManager.BusinessLayer
{
    public class Movie : FileManagerObjectBase, IVideo
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

        //public void Save()
        //{
        //    _commandText = "dbo.MovieSave";
        //    var fileManagerDb = GetDb();
        //    using (var connection = fileManagerDb.CreateConnection())
        //    using (var command = fileManagerDb.CreateCommand())
        //    {
        //        connection.Open();
        //        command.Parameters.AddWithValue("@MovieId", this.MovieId);
        //        command.Parameters.AddWithValue("@SeriesId", this.SeriesId);
        //        command.Parameters.AddWithValue("@MovieName", this.Name);
        //        command.Parameters.AddWithValue("@IsSeries", this.IsSeries);
        //        command.Parameters.AddWithValue("@MovieFormat", this.Format);
        //        command.Parameters.AddWithValue("@MovieCategory", this.Category);
        //        command.Parameters.AddWithValue("@Path", this.Path);

        //        command.ExecuteNonQuery();
        //    }
        //}

        //public static IEnumerable<Movie> GetMovies()
        //{
        //    var movies = new List<Movie>();

        //    _commandText = "dbo.MovieGetList";
        //    var fileManagerDb = GetDb();
        //    using (var connection = fileManagerDb.CreateConnection())
        //    using (var command = fileManagerDb.CreateCommand())
        //    {
        //        connection.Open();
        //        var reader = command.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            movies.Add(new Movie
        //            {
        //                MovieId = (int)reader["MovieId"],
        //                SeriesId = (int)reader["SeriesId"],
        //                Name = (string)reader["MovieName"],
        //                IsSeries = (bool)reader["IsSeries"],
        //                Format = (string)reader["MovieFormat"],
        //                Category = (string)reader["MovieCategory"],
        //                Path = (string)reader["FilePath"]
        //            });
        //        }
        //    }

        //    return movies;
        //}

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