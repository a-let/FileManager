using FileManager.Models;
using FileManager.Web.Services.Interfaces;
using FileManager.DataAccessLayer.Interfaces;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Web.Services
{
    public class MovieControllerService : IControllerService<Movie>
    {
        private readonly IRepository<Movie> _movieRepository;

        public MovieControllerService(IRepository<Movie> movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<Movie> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid MovieId");

            return await _movieRepository.GetByIdAsync(id);
        }

        public Movie GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            return _movieRepository.GetByName(name);
        }

        public IEnumerable<Movie> Get() =>
            _movieRepository.Get();

        public async Task SaveAsync(Movie movie)
        {
            if (movie == null)
                throw new ArgumentNullException(nameof(movie));

            await _movieRepository.SaveAsync(movie);
        }
    }
}