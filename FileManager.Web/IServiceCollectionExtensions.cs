using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FileManager.Web
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddSqlServer(configuration["FileManagerConnectionString"], name: "FileManager Database Check", tags: new[] { "filemanagerdb" });

            services.AddHealthChecksUI();

            return services;
        }
    }
}