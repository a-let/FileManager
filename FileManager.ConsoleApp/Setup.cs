using FileManager.Interfaces;
using FileManager.Services;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Polly;
using Polly.Extensions.Http;

using Serilog;
using Serilog.Events;

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
            Log.Logger = GetLoggerConfiguration()
                .CreateLogger();

            try
            {
                Log.Logger.Information("{AssemblyName}: Starting console");

                var services = new ServiceCollection()
                    .AddLogging(builder =>
                    {
                        builder.AddSerilog(logger: Log.Logger, dispose: true);
                    })
                    .AddSingleton<IConfiguration, ConfigurationRoot>(configuration => (ConfigurationRoot)GetConfiguration())
                    .AddSingleton<IFileManagerClient, FileManagerClient>();

                services
                    .AddHttpClient("FileManager", c =>
                    {
                        c.BaseAddress = new Uri(_config["FileManagerBaseAddress"]);
                        c.DefaultRequestHeaders.Clear();
                        c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    })
                    .AddPolicyHandler((s,r) =>
                    {
                        return HttpPolicyExtensions.HandleTransientHttpError()
                            .WaitAndRetryAsync(new[]
                            {
                                TimeSpan.FromSeconds(1),
                                TimeSpan.FromSeconds(5),
                                TimeSpan.FromSeconds(10)
                            },
                            onRetry: (result, timespan, retryAttempt, conext) =>
                            {
                                var logger = s.GetService<ILogger<Setup>>();

                                var message = $"Retry Attempt: {retryAttempt}";

                                if (result.Exception != null)
                                    logger.LogWarning(result.Exception, message);
                                else
                                    logger.LogWarning(message);
                            });
                    });

                return services.BuildServiceProvider();
            }
            catch (Exception ex)
            {
                Log.Logger.Fatal(ex, "{AssemblyName}: Console terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }

            return null;
        }

        private static LoggerConfiguration GetLoggerConfiguration()
        {
            var loggerConfig = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("AssemblyName", Assembly.GetExecutingAssembly().GetName().Name)
                .WriteTo.Console();

            return loggerConfig;
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