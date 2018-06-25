using System;
using System.Collections.Generic;

using FileManager.BusinessLayer.Interfaces;
using FileManager.Models;

namespace FileManager.BusinessLayer.Adapters
{
    public class SeriesAdapter : IFileManagerObjectAdapter<Series>
    {
        private readonly IFileManagerDb _fileManagerDb;

        public SeriesAdapter(IFileManagerDb fileManagerDb)
        {
            _fileManagerDb = fileManagerDb;
        }

        public IEnumerable<Series> Get()
        {
            var series = new List<Series>();

            using (var connection = _fileManagerDb.CreateConnection())
            using (var command = _fileManagerDb.CreateCommand())
            {
                connection.Open();
                command.CommandText = "dbo.SeriesGetList";
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    series.Add(new Series
                    {
                        SeriesId = (int)reader["SeriesId"],
                        Name = (string)reader["SeriesName"],
                        Path = (string)reader["FilePath"]
                    });
                }
            }

            return series;
        }

        public Series GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Series GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Series> GetByParentId(int parentId)
        {
            throw new NotImplementedException();
        }

        public bool Save(Series target)
        {
            try
            {
                using (var connection = _fileManagerDb.CreateConnection())
                using (var command = _fileManagerDb.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = "dbo.SeriesSave";
                    command.Parameters.Add(_fileManagerDb.CreateParameter("@SeriesId", target.SeriesId));
                    command.Parameters.Add(_fileManagerDb.CreateParameter("@SeriesName", target.Name));
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