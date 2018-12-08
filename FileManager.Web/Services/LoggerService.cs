using FileManager.DataAccessLayer.Interfaces;
using FileManager.Models;
using Microsoft.Extensions.Logging;
using System;

namespace FileManager.Web.Services
{
    public class LoggerService : ILogger
    {
        private readonly ILogRepository _logRepository;

        public LoggerService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public IDisposable BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state,
            Exception exception, Func<TState, Exception, string> formatter) =>
            _logRepository.SaveLog(new Log
            {
                LogId = 0,
                LogLevel = logLevel.ToString(),
                ExceptionType = exception.GetType().ToString(),
                Message = formatter != null ? formatter(state, exception) : Format(logLevel, exception),
                StackTrace = exception?.StackTrace ?? string.Empty,
                CreatedDate = DateTime.Now
            });

        private string Format(LogLevel logLevel, Exception exception) =>
            exception?.Message ?? string.Empty;
    }
}