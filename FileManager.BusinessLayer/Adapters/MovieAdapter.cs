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
                using (var command = (System.Data.SqlClient.SqlCommand)_fileManagerDb.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = "dbo.MovieSave";
                    command.Parameters.AddWithValue("@MovieId", target.MovieId);
                    command.Parameters.AddWithValue("@SeriesId", target.SeriesId);
                    command.Parameters.AddWithValue("@MovieName", target.Name);
                    command.Parameters.AddWithValue("@IsSeries", target.IsSeries);
                    command.Parameters.AddWithValue("@MovieFormat", target.Format);
                    command.Parameters.AddWithValue("@MovieCategory", target.Category);
                    command.Parameters.AddWithValue("@Path", target.Path);

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