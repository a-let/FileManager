using System;
using System.Collections.Generic;

using FileManager.BusinessLayer;
using FileManager.UI.Interfaces;

namespace FileManager.UI.ViewModels
{
    public class MovieListWindowViewModel : IFileManagerViewModel
    {
        public Action<IFileManagerViewModel> OpenWindow { get; set; }

        public Movie Movie { get; set; }
        public IEnumerable<Movie> MovieList { get; set; }

        public MovieListWindowViewModel()
        {
            MovieList = Movie.GetMovies();
        }
    }
}