using System;
using System.Collections.Generic;

using FileManager.BusinessLayer.Interfaces;
using FileManager.Models;

namespace FileManager.BusinessLayer.Adapters
{
    public class ShowAdapter : IFileManagerObjectAdapter<Show>
    {
        private readonly IFileManagerDb _fileManagerDb;

        public ShowAdapter(IFileManagerDb fileManagerDb)
        {
            _fileManagerDb = fileManagerDb;
        }

        public IEnumerable<Show> Get()
        {
            var shows = new List<Show>();

            using (var connection = _fileManagerDb.CreateConnection())
            using (var command = _fileManagerDb.CreateCommand())
            {
                connection.Open();
                command.CommandText = "dbo.ShowGetList";
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    shows.Add(new Show
                    {
                        ShowId = (int)reader["ShowId"],
                        Name = (string)reader["ShowName"],
                        Category = (string)reader["ShowCategory"],
                        Path = (string)reader["FilePath"]
                    });
                }
            }

            return shows;
        }

        public Show GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Show GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Show> GetByParentId(int parentId)
        {
            throw new NotImplementedException();
        }

        public bool Save(Show target)
        {
            try
            {
                using (var connection = _fileManagerDb.CreateConnection())
                using (var command = (System.Data.SqlClient.SqlCommand)_fileManagerDb.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = "dbo.ShowSave";
                    command.Parameters.AddWithValue("@ShowId", target.ShowId);
                    command.Parameters.AddWithValue("@Name", target.Name);
                    command.Parameters.AddWithValue("@Category", target.Category);
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