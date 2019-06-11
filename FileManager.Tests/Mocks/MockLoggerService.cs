using FileManager.Web.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FileManager.Tests.Mocks
{
    public class MockLoggerService : ILogger
    {
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            // Intentionally left empty
        }
    }

    public class MockLog : ILog
    {
        public bool IsEnabled => throw new NotImplementedException();

        public void Log(LogLevel logLevel, Exception exception, Func<Exception, string> formatter)
        {
            throw new NotImplementedException();
        }

        public Task LogAsync(LogLevel logLevel, Exception exception, Func<Exception, string> formatter)
        {
            throw new NotImplementedException();
        }
    }
}