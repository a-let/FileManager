using FileManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileManager.DataAccessLayer.Interfaces
{
    public interface IMovieRepository : IDisposable
    {
        Movie GetMovieById(int id);
        IEnumerable<Movie> GetMovies();
        Movie GetMovieByName(string name);
        bool SaveMovie(Movie movie);
        IQueryable<Movie> GetMoviesBySeriesId(int seriesId);
    }
}