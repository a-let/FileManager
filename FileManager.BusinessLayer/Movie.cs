using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        public static Movie NewMovie()
        {
            using (var context = new FileManagerContext())
            {
                var movie = new Movie();
                movie = context.Movie.Create();
                return movie;
            }
        }

        public async void SaveAsync()
        {
            using (var context = new FileManagerContext())
            {
                context.Movie.Add(this);

                context.Entry(this).State = this.MovieId == 0 ? EntityState.Added : EntityState.Modified;

                await context.SaveChangesAsync();
            }
        }

        public static IEnumerable<Movie> GetMovies()
        {
            var movie = new List<Movie>();

            using (var context = new FileManagerContext())
            {
                foreach (var m in context.Movie)
                {
                    movie.Add(new Movie()
                    {
                        MovieId = m.MovieId,
                        SeriesId = m.SeriesId,
                        Name = m.Name,
                        IsSeries = m.IsSeries,
                        Format = m.Format,
                        Category = m.Category,
                        Path = m.Path
                    });
                }
            }

            return movie;
        }

        public static Movie GetMovie(string name)
        {
            var movie = new Movie();

            using (var context = new FileManagerContext())
            {
                movie = context.Movie
                    .SingleOrDefault(e => e.Name.Equals(name, System.StringComparison.OrdinalIgnoreCase));
            }

            return movie;
        }

        public static IEnumerable<Movie> GetMoviesBySeriesId(int id)
        {
            var movies = new List<Movie>();

            using (var context = new FileManagerContext())
            {
                foreach (var m in context.Movie.Where(m => m.IsSeries && m.SeriesId == id))
                {
                    movies.Add(new Movie()
                    {
                        MovieId = m.MovieId,
                        SeriesId = m.SeriesId,
                        Name = m.Name,
                        IsSeries = m.IsSeries,
                        Format = m.Format,
                        Category = m.Category,
                        Path = m.Path
                    });
                }                
            }

            return movies;
        }
    }
}