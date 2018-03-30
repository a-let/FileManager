using FileManager.BusinessLayer;
using FileManager.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.UI.ViewModels
{
    public class MovieListWindowViewModel : IFileManagerViewModel
    {
        public Action OpenWindow { get; set; }

        public Movie Movie { get; set; }
        public IEnumerable<Movie> MovieList { get; set; }

        public MovieListWindowViewModel()
        {
            MovieList = Movie.GetMovies();
        }
    }
}