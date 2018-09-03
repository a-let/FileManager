using FileManager.Models;
using FileManager.Web.Services.Interfaces;
using FileManager.DataAccessLayer;

using System;
using System.Collections.Generic;
using System.Linq;

namespace FileManager.Web.Services
{
    public class MovieControllerService : IMovieControllerService
    {
        private readonly FileManagerContext _fileManagerContext;

        public MovieControllerService(FileManagerContext fileManagerContext)
        {
            _fileManagerContext = fileManagerContext;
        }

        public Movie GetMovieById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid MovieId");

            return _fileManagerContext.Movies.Find(id);
        }

        public Movie GetMovieByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            return _fileManagerContext.Movies.Single(m => m.Name.Equals(name));
        }

        public IEnumerable<Movie> GetMovies()
        {
            return _fileManagerContext.Movies;
        }

        public IQueryable<Movie> GetMoviesBySeriesId(int seriesId)
        {
            if (seriesId < 0)
                throw new ArgumentException("Invalid SeriesId");

            return _fileManagerContext.Movies.Where(m => m.SeriesId == seriesId);
        }

        public void SaveMovie(Movie movie)
        {
            if (movie == null)
                throw new ArgumentNullException(nameof(movie));

            if (movie.MovieId == 0)
                _fileManagerContext.Movies.Add(movie);
            else
            {
                var m = _fileManagerContext.Movies.Find(movie.MovieId);
                m.SeriesId = movie.SeriesId;
                m.Name = movie.Name;
                m.IsSeries = movie.IsSeries;
                m.Format = movie.Format;
                m.Category = m.Category;
                m.Path = movie.Path;
            }

            _fileManagerContext.SaveChanges();
        }
    }
}