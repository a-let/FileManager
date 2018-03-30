using System.Collections.Generic;
using System.Linq;
using FileManager.BusinessLayer;
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

        private Movie() { }

        public static Movie NewMovie() => new Movie();

        public async void Save()
        {
            using (var context = new FileManagerContext())
            {
                context.Movie.Add(this);
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

        public static Movie FindMovie(string name)
        {
            var movie = new Movie();

            using (var context = new FileManagerContext())
            {
                movie = context.Movie
                    .SingleOrDefault(e => e.Name.Equals(name, System.StringComparison.OrdinalIgnoreCase));
            }

            return movie;
        }
    }
}