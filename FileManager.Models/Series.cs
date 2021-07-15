using System.Collections.Generic;

namespace FileManager.Models
{
    public class Series
    {
        public int SeriesId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }

        public List<Movie> Movies { get; set; }
    }
}