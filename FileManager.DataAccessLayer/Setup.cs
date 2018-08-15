using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FileManager.DataAccessLayer
{
    public static class Setup
    {
        public static ServiceProvider CreateServices()
        {
            var connection = @"Server=(LocalDb)\MSSQLLocalDB;Database=FileManager;Trusted_Connection=True;ConnectRetryCount=0";

            var services = new ServiceCollection()
                .AddDbContext<FileManagerContext>(o => o.UseSqlServer(connection))
                .BuildServiceProvider();

            return services;
        }
    }
}