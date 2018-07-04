using System;
using System.Collections.Generic;
using System.Data;
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
                    series.Add(CreateFromReader(reader));
                }
            }

            return series;
        }

        public Series GetById(int id)
        {
            Series series = null;

            using (var connection = _fileManagerDb.CreateConnection())
            using (var command = _fileManagerDb.CreateCommand())
            {
                connection.Open();
                command.CommandText = "dbo.SeriesGetById";
                command.Parameters.Add(_fileManagerDb.CreateParameter("@SeriesId", id));

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        series = CreateFromReader(reader);
                    }
                }
            }

            return series;
        }

        public Series GetByName(string name)
        {
            Series series = null;

            using (var connection = _fileManagerDb.CreateConnection())
            using (var command = _fileManagerDb.CreateCommand())
            {
                connection.Open();
                command.CommandText = "dbo.SeriesGetByName";
                command.Parameters.Add(_fileManagerDb.CreateParameter("@SeriesName", name));

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        series = CreateFromReader(reader);
                    }
                }
            }

            return series;
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

        private Series CreateFromReader(IDataReader reader) => new Series
        {
            SeriesId = (int)reader["SeriesId"],
            Name = (string)reader["SeriesName"],
            Path = (string)reader["FilePath"]
        };
    }
}