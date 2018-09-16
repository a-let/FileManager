using FileManager.Models;
using FileManager.Web.Services.Interfaces;
using FileManager.DataAccessLayer.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;

namespace FileManager.Web.Services
{
    public class MovieControllerService : IMovieControllerService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieControllerService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public Movie GetMovieById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid MovieId");

            return _movieRepository.GetMovieById(id);
        }

        public Movie GetMovieByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            return _movieRepository.GetMovieByName(name);
        }

        public IEnumerable<Movie> GetMovies()
        {
            return _movieRepository.GetMovies();
        }

        public IQueryable<Movie> GetMoviesBySeriesId(int seriesId)
        {
            if (seriesId < 0)
                throw new ArgumentException("Invalid SeriesId");

            return _movieRepository.GetMoviesBySeriesId(seriesId);
        }

        public bool SaveMovie(Movie movie)
        {
            if (movie == null)
                throw new ArgumentNullException(nameof(movie));

            return _movieRepository.SaveMovie(movie);
        }
    }
}