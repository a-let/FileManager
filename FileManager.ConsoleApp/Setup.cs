using FileManager.Interfaces;
using FileManager.Services;

using Logging;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Polly;
using Polly.Extensions.Http;

using System;
using System.IO;
using System.Net.Http.Headers;
using System.Reflection;

namespace FileManager.ConsoleApp
{
    public class Setup
    {
        private static IConfiguration _config;

        public static ServiceProvider CreateServices()
        {
            var services = new ServiceCollection()
                .AddSingleton<IConfiguration, ConfigurationRoot>(configuration => (ConfigurationRoot)GetConfiguration())
                .AddSingleton<IFileManagerClient, FileManagerClient>();

            services.ConfigureLogging(Assembly.GetEntryAssembly().GetName().Name);

            services
                .AddHttpClient("FileManager", c =>
                {
                    c.BaseAddress = new Uri(_config["FileManagerBaseAddress"]);
                    c.DefaultRequestHeaders.Clear();
                    c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                })
                .AddPolicyHandler((s, r) => HttpPolicyExtensions.HandleTransientHttpError()
                    .WaitAndRetryAsync(new[]
                    {
                        TimeSpan.FromSeconds(1),
                        TimeSpan.FromSeconds(5),
                        TimeSpan.FromSeconds(10)
                    },
                    onRetryAsync: async (result, timespan, retryAttempt, context) =>
                    {
                        var logger = s.GetService<ILogger>();
                        await logger.LogWarningAsync($"Timeout #{retryAttempt}: {result.Exception.Message}", result.Exception);
                    }));


            return services.BuildServiceProvider();
        }

        private static IConfiguration GetConfiguration()
        {
            _config =  new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            return _config;
        }
    }
}