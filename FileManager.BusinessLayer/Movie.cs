using System.Collections.Generic;

using FileManager.BusinessLayer.Interfaces;

namespace FileManager.BusinessLayer
{
    public class Movie : FileManagerObjectBase, IFileManagerObject, IVideo
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
            _commandText = "dbo.MovieSave";
            using (var connection = _fileManagerDb.CreateConnection())
            using (var command = _fileManagerDb.CreateCommand(_commandText))
            {
                connection.Open();

                _paramDict = new Dictionary<string, object>
                {
                    { "@MovieId", this.MovieId },
                    { "@SeriesId", this.SeriesId },
                    { "@MovieName", this.Name },
                    { "@IsSeries", this.IsSeries },
                    { "@MovieFormat", this.Format },
                    { "@MovieCategory", this.Category },
                    { "@Path", this.Path }
                };

                _fileManagerDb.AddParameters(_paramDict);

                command.ExecuteNonQuery();
            }
        }

        public static IEnumerable<Movie> GetMovies()
        {
            var movies = new List<Movie>();

            _commandText = "dbo.MovieGetList";
            using (var connection = _fileManagerDb.CreateConnection())
            using (var command = _fileManagerDb.CreateCommand(_commandText))
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