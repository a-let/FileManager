using Microsoft.Extensions.Logging;
using System;

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
}