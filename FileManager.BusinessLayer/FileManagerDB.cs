using System.Data;
using System.Data.SqlClient;

using Microsoft.Extensions.Configuration;

using FileManager.BusinessLayer.Interfaces;

namespace FileManager.BusinessLayer
{
    public class FileManagerDb : IFileManagerDb
    {

        private readonly IConfiguration _configuration;
        private IDbConnection _connection;
        private IDbCommand _command;

        public FileManagerDb(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {
            _connection = new SqlConnection(_configuration["FileManagerConnectionString"]);
            return _connection;
        }

        public IDbCommand CreateCommand()
        {
            _command = new SqlCommand() { CommandType = CommandType.StoredProcedure, Connection = (SqlConnection)_connection };
            return _command;
        }

        public IDbDataParameter CreateParameter(string name, object value)
        {
            var parameter = _command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;

            return parameter;
        }
    }
}