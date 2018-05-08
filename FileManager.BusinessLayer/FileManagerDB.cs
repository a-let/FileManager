using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

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

        private static SqlCommand _createdCommand;
        public SqlCommand CreateCommand(string commandText)
        {
            _createdCommand = new SqlCommand(commandText, _createdConnection) { CommandType = CommandType.StoredProcedure };
            return _createdCommand;
        }

        public void AddParameters(IDictionary<string, object> paramDict) => paramDict.Select(p => _createdCommand.Parameters.AddWithValue(p.Key, p.Value));
    }    
}