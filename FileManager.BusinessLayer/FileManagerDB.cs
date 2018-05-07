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

        private static SqlConnection _createdConnection;
        public SqlConnection CreateConnection()
        {
            _createdConnection = new SqlConnection(_connection.ConnectionString);
            return _createdConnection;
        }

        public SqlCommand CreateCommand(string commandText) => new SqlCommand(commandText, _createdConnection) { CommandType = CommandType.StoredProcedure };
    }    
}