using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Microsoft.Extensions.DependencyInjection;

using FileManager.BusinessLayer.Interfaces;

namespace FileManager.BusinessLayer
{
    public class Setup
    {
        private static SqlConnection _connection;

        public static ServiceProvider CreateServices(string commandName)
        {
            var services = new ServiceCollection()
                .AddSingleton<IDbConnection, SqlConnection>(connection => GetSqlConnection())
                .AddSingleton<IDbCommand, SqlCommand>(command => GetSqlCommand(commandName))
                .AddSingleton<IFileManagerDb, FileManagerDb>()
                .BuildServiceProvider();

            return services;
        }

        private static SqlConnection GetSqlConnection()
        {
            _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FileManager"].ConnectionString);

            return _connection;
        }

        private static SqlCommand GetSqlCommand(string commandText) => new SqlCommand(commandText, _connection) { CommandType = CommandType.StoredProcedure };
    }
}