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
            _command = (SqlCommand)command;
        }

        public SqlConnection CreateConnection()
        {
            return (SqlConnection)_connection;
        }

        public SqlCommand CreateCommand()
        {
            return (SqlCommand)_command;
        }
    }    
}