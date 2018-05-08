using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Microsoft.Extensions.DependencyInjection;

using FileManager.BusinessLayer.Interfaces;

namespace FileManager.BusinessLayer
{
    public static class Setup
    {
        private static IDbConnection _connection;
        
        public static ServiceProvider CreateServices(string commandText)
        {
            var services = new ServiceCollection()
                .AddSingleton<IDbConnection, SqlConnection>(connection => GetSqlConnection())
                .AddSingleton<IDbCommand, SqlCommand>(command => GetSqlCommand(commandText))
                .AddSingleton<IFileManagerDb, FileManagerDb>()
                .BuildServiceProvider();

            return services;
        }

        private static SqlConnection GetSqlConnection()
        {
            _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FileManager"].ConnectionString);
            return (SqlConnection)_connection;
        }

        private static SqlCommand GetSqlCommand(string commandText) => new SqlCommand(commandText, (SqlConnection)_connection) { CommandType = CommandType.StoredProcedure };
    }
}