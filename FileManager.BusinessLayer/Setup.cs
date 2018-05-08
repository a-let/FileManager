using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Microsoft.Extensions.DependencyInjection;

using FileManager.BusinessLayer.Interfaces;
using System.Collections.Generic;

namespace FileManager.BusinessLayer
{
    public static class Setup
    {
        private static IDbConnection _connection;
        private static IDbCommand _command;

        public static ServiceProvider CreateServices(string commandText, IDictionary<string, object> paramDict)
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

        private static SqlCommand GetSqlCommand(string commandText)
        {
            _command = new SqlCommand(commandText, (SqlConnection)_connection) { CommandType = CommandType.StoredProcedure };
            return (SqlCommand)_command;
        }

        private static void AddSqlParameters(IDictionary<string, object> paramDict)
        {
            foreach(var param in paramDict)
            {
                _command.Parameters.Add(new SqlParameter(param.Key, param.Value));
            }            
        }
    }
}