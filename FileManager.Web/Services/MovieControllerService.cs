using System;
using System.Collections.Generic;
using FileManager.Models;
using FileManager.BusinessLayer.Interfaces;
using FileManager.Web.Services.Interfaces;

namespace FileManager.Web.Services
{
    public class MovieControllerService : IMovieControllerService
    {
        private readonly IFileManagerObjectRepository<Movie> _movieRepository;

        public MovieControllerService(IFileManagerObjectRepository<Movie> movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public Movie GetMovieById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid MovieId");

            return _movieRepository.GetById(id);
        }

        public Movie GetMovieByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            return _movieRepository.GetByName(name);
        }

        public IEnumerable<Movie> GetMovies()
        {
            return _movieRepository.Get();
        }

        public IEnumerable<Movie> GetMoviesBySeriesId(int seriesId)
        {
            if (seriesId < 0)
                throw new ArgumentException("Invalid SeriesId");

            return _movieRepository.GetByParentId(seriesId);
        }

        public bool SaveMovie(Movie movie)
        {
            if (movie == null)
                throw new ArgumentNullException(nameof(movie));

            return _movieRepository.Save(movie);
        }
    }
}