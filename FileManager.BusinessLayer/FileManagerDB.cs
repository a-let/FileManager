using System.Data;
using System.Data.SqlClient;

using FileManager.BusinessLayer.Interfaces;

namespace FileManager.BusinessLayer
{    
    public class FileManagerDb : IFileManagerDb
    {
        private readonly IDbConnection _connection;

        public FileManagerDb(IDbConnection connection)
        {
            _connection = connection;
        }

        public SqlConnection CreateConnection() => new SqlConnection(_connection.ConnectionString);
    }    
}