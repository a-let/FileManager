using FileManager.DataAccessLayer.Interfaces;
using FileManager.Models;
using FileManager.Web.Services.Interfaces;

using Microsoft.Extensions.Logging;

using System;
using System.Threading.Tasks;

namespace FileManager.Web.Services
{
    public class LoggerService : ILog
    {
        private readonly ILogRepository _logRepository;

        public bool IsEnabled { get; }

        public LoggerService(ILogRepository logRepository)
        {
            _logRepository = logRepository;

            IsEnabled = true;
        }

        [Obsolete("Use ILog and LogAsync.")]
        public void Log(LogLevel logLevel, Exception exception, Func<Exception, string> formatter) =>
            _logRepository.SaveLogAsync(new Log
            {
                LogId = 0,
                LogLevel = logLevel.ToString(),
                ExceptionType = exception.GetType().ToString(),
                Message = formatter != null ? formatter(exception) : exception.Message,
                StackTrace = exception?.StackTrace ?? string.Empty,
                CreatedDate = DateTime.Now
            }).GetAwaiter().GetResult();

        public async Task LogAsync(LogLevel logLevel, Exception exception, Func<Exception, string> formatter) =>
            await _logRepository.SaveLogAsync(new Log
            {
                LogId = 0,
                LogLevel = logLevel.ToString(),
                ExceptionType = exception.GetType().ToString(),
                Message = formatter != null ? formatter(exception) : exception.Message,
                StackTrace = exception?.StackTrace ?? string.Empty,
                CreatedDate = DateTime.Now
            });
    }
}