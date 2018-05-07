using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Microsoft.Extensions.DependencyInjection;

using FileManager.BusinessLayer.Interfaces;

namespace FileManager.BusinessLayer
{
    public class Setup
    {
        public static ServiceProvider CreateServices()
        {
            var services = new ServiceCollection()
                .AddSingleton<IDbConnection, SqlConnection>(c => GetSqlConnection())
                .AddSingleton<IFileManagerDb, FileManagerDb>()
                .BuildServiceProvider();

            return services;
        }

        private static SqlConnection GetSqlConnection() => new SqlConnection(ConfigurationManager.ConnectionStrings["FileManager"].ConnectionString);
    }
}