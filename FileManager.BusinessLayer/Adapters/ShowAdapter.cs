using System;
using System.Collections.Generic;
using System.Data;
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
                    shows.Add(CreateFromReader(reader));
                }
            }

            return shows;
        }

        public Show GetById(int id)
        {
            Show show = null;

            using (var connection = _fileManagerDb.CreateConnection())
            using (var command = _fileManagerDb.CreateCommand())
            {
                connection.Open();
                command.CommandText = "dbo.ShowGetById";
                command.Parameters.Add(_fileManagerDb.CreateParameter("@ShowId", id));

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        show = CreateFromReader(reader);
                    }
                }
            }

            return show;
        }

        public Show GetByName(string name)
        {
            Show show = null;

            using (var connection = _fileManagerDb.CreateConnection())
            using (var command = _fileManagerDb.CreateCommand())
            {
                connection.Open();
                command.CommandText = "dbo.ShowGetByName";
                command.Parameters.Add(_fileManagerDb.CreateParameter("@ShowName", name));

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        show = CreateFromReader(reader);
                    }
                }
            }

            return show;
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
                using (var command = _fileManagerDb.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = "dbo.ShowSave";
                    command.Parameters.Add(_fileManagerDb.CreateParameter("@ShowId", target.ShowId));
                    command.Parameters.Add(_fileManagerDb.CreateParameter("@Name", target.Name));
                    command.Parameters.Add(_fileManagerDb.CreateParameter("@Category", target.Category));
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

        private Show CreateFromReader(IDataReader reader) => new Show
        {
            ShowId = (int)reader["ShowId"],
            Name = (string)reader["ShowName"],
            Category = (string)reader["ShowCategory"],
            Path = (string)reader["FilePath"]
        };
    }
}