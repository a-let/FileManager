using System.Data;
using System.Data.SqlClient;

using FileManager.BusinessLayer.Interfaces;
using Microsoft.Extensions.Configuration;

namespace FileManager.BusinessLayer
{
    public class FileManagerDb : IFileManagerDb
    {

        private readonly IConfiguration _configuration;
        private IDbConnection _connection;

        public FileManagerDb(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlCommand CreateCommand()
        {
            return new SqlCommand() { CommandType = CommandType.StoredProcedure, Connection = (SqlConnection)_connection };
        }

        public SqlConnection CreateConnection()
        {
            _connection = new SqlConnection(_configuration["FileManagerConnectionString"]);
            return (SqlConnection)_connection;
        }
    }
}