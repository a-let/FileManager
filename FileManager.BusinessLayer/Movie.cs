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
    }
}