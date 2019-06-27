using Microsoft.Extensions.Logging;

using System;
using System.Threading.Tasks;

namespace FileManager.Web.Services.Interfaces
{
    public static class ILogExtensions
    {
        public static async Task LogErrorAsync(this ILog log, Exception exception, string message)
        {
            await log.LogAsync(LogLevel.Error, exception, ex => Format(ex, message));
        }

        public static async Task LogWarningAsync(this ILog log, string message, Exception exception = null)
        {
            await log.LogAsync(LogLevel.Warning, exception, ex => Format(ex, message));
        }

        public static async Task LogInfoAsync(this ILog log, string message)
        {
            await log.LogAsync(LogLevel.Information, null, ex => Format(ex, message));
        }

        public static async Task LogDebugAsync(this ILog log, string message, Exception exception = null)
        {
            await log.LogAsync(LogLevel.Debug, exception, ex => Format(ex, message));
        }

        private static string Format(Exception exception, string message) =>
            string.IsNullOrWhiteSpace(message) ? exception?.Message : message;
    }
}
