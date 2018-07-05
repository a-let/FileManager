using System;
using System.Collections.Generic;
using FileManager.Models;
using FileManager.BusinessLayer.Interfaces;

namespace FileManager.Web.Services
{
    public class MovieControllerService : IMovieControllerService
    {
        private readonly IFileManagerObjectAdapter<Movie> _movieAdapter;

        public MovieControllerService(IFileManagerObjectAdapter<Movie> movieAdapter)
        {
            _movieAdapter = movieAdapter;
        }

        public Movie GetMovieById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid MovieId");

            return _movieAdapter.GetById(id);
        }

        public Movie GetMovieByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            return _movieAdapter.GetByName(name);
        }

        public IEnumerable<Movie> GetMovies()
        {
            return _movieAdapter.Get();
        }

        public IEnumerable<Movie> GetMoviesBySeriesId(int seriesId)
        {
            if (seriesId < 0)
                throw new ArgumentException("Invalid SeriesId");

            return _movieAdapter.GetByParentId(seriesId);
        }

        public bool SaveMovie(Movie movie)
        {
            if (movie == null)
                throw new ArgumentNullException(nameof(movie));

            return _movieAdapter.Save(movie);
        }
    }
}