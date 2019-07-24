using Microsoft.Extensions.Logging;

using System;
using System.Threading.Tasks;

namespace FileManager.Tests.Mocks
{
    public class MockLog : Logging.ILogger
    {
        public bool IsEnabled => throw new NotImplementedException();

        public void Log(LogLevel logLevel, Exception exception, Func<Exception, string> formatter)
        {
            // Intentionally left empty
        }

        public Task LogAsync(LogLevel logLevel, Exception exception, Func<Exception, string> formatter) => Task.CompletedTask;
    }
}