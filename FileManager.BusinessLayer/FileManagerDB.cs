using System.Data;
using System.Data.SqlClient;

using FileManager.BusinessLayer.Interfaces;

namespace FileManager.BusinessLayer
{
    public class FileManagerDb : IFileManagerDb
    {
        private readonly IDbConnection _connection;
        private readonly IDbCommand _command;

        public FileManagerDb(IDbConnection connection, IDbCommand command)
        {
            _connection = connection;
            _command = command;
        }

        public SqlConnection CreateConnection() => (SqlConnection)_connection;

        public SqlCommand CreateCommand() => (SqlCommand)_command;
    }    
}