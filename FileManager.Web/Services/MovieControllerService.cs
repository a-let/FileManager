using FileManager.Models;
using FileManager.Web.Services.Interfaces;
using FileManager.DataAccessLayer.Interfaces;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Web.Services
{
    public class MovieControllerService : IMovieControllerService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieControllerService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<Movie> GetAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid MovieId");

            return await _movieRepository.GetMovieByIdAsync(id);
        }

        public async Task<Movie> GetAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            return await Task.Run(() => _movieRepository.GetMovieByName(name));
        }

        public async Task<IEnumerable<Movie>> GetAsync() => await Task.Run(() => _movieRepository.GetMovies());

        public IEnumerable<Movie> GetMoviesBySeriesId(int seriesId)
        {
            if (seriesId < 0)
                throw new ArgumentException("Invalid SeriesId");

            return _movieRepository.GetMoviesBySeriesId(seriesId);
        }

        public async Task<int> SaveAsync(Movie movie)
        {
            if (movie == null)
                throw new ArgumentNullException(nameof(movie));

            return await _movieRepository.SaveMovieAsync(movie);
        }
    }
}