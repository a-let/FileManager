using System;
using System.Collections.Generic;

using FileManager.BusinessLayer.Interfaces;
using FileManager.Models;

namespace FileManager.BusinessLayer.Adapters
{
    public class MovieAdapter : IFileManagerObjectAdapter<Movie>
    {
        private readonly IFileManagerDb _fileManagerDb;

        public MovieAdapter(IFileManagerDb fileManagerDb)
        {
            _fileManagerDb = fileManagerDb;
        }

        public IEnumerable<Movie> Get()
        {
            var movies = new List<Movie>();

            using (var connection = _fileManagerDb.CreateConnection())
            using (var command = _fileManagerDb.CreateCommand())
            {
                connection.Open();
                command.CommandText = "dbo.MovieGetList";
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    movies.Add(new Movie
                    {
                        MovieId = (int)reader["MovieId"],
                        SeriesId = (int)reader["SeriesId"],
                        Name = (string)reader["MovieName"],
                        IsSeries = (bool)reader["IsSeries"],
                        Format = (string)reader["MovieFormat"],
                        Category = (string)reader["MovieCategory"],
                        Path = (string)reader["FilePath"]
                    });
                }
            }

            return movies;
        }

        public Movie GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Movie GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Movie> GetByParentId(int parentId)
        {
            throw new NotImplementedException();
        }

        public bool Save(Movie target)
        {
            try
            {
                using (var connection = _fileManagerDb.CreateConnection())
                using (var command = _fileManagerDb.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = "dbo.MovieSave";
                    command.Parameters.Add(_fileManagerDb.CreateParameter("@MovieId", target.MovieId));
                    command.Parameters.Add(_fileManagerDb.CreateParameter("@SeriesId", target.SeriesId));
                    command.Parameters.Add(_fileManagerDb.CreateParameter("@MovieName", target.Name));
                    command.Parameters.Add(_fileManagerDb.CreateParameter("@IsSeries", target.IsSeries));
                    command.Parameters.Add(_fileManagerDb.CreateParameter("@MovieFormat", target.Format));
                    command.Parameters.Add(_fileManagerDb.CreateParameter("@MovieCategory", target.Category));
                    command.Parameters.Add(_fileManagerDb.CreateParameter("@Path", target.Path));

                    command.ExecuteNonQuery();
                }

                return true;
            }
            catch
            {
                return false;
            }
            
        }
    }
}