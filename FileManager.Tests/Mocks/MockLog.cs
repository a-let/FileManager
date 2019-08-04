using Microsoft.Extensions.Logging;

using System;
using System.Threading.Tasks;

namespace FileManager.Tests.Mocks
{
    public class MockLog : Logging.ILogger
    {
        public bool IsEnabled => true;

        public Task LogAsync(LogLevel logLevel, Exception exception, Func<Exception, string> formatter) => Task.CompletedTask;
    }
}