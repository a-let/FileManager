using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;

using System.Text;

using Xunit.Abstractions;
using Xunit.Sdk;

namespace Serilog
{
    public class XUnitTestSink : ILogEventSink
    {
        private readonly IMessageSink _messageSink;

        public XUnitTestSink(IMessageSink messageSink)
        {
            _messageSink = messageSink;
        }

        public void Emit(LogEvent logEvent)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0} : {1} : {2}",
                logEvent.Timestamp.DateTime,
                logEvent.Level,
                logEvent.RenderMessage());

            if (logEvent.Exception != null)
            {
                sb.AppendLine();
                sb.Append(logEvent.Exception);
            }

            _messageSink.OnMessage(new DiagnosticMessage(sb.ToString()));
        }
    }

    public static class XUnitTestSinkExtensions
    {
        /// <summary>
        /// Requires XUnit Diagnostic Messages to be enabled via configuration file.
        /// See <see href="https://xunit.net/docs/capturing-output">XUnit Capturing Output</see>.
        /// </summary>
        /// <param name="loggerSinkConfiguration"></param>
        /// <param name="messageSink"></param>
        /// <returns></returns>
        public static LoggerConfiguration XUnitTestSink(this LoggerSinkConfiguration loggerSinkConfiguration, IMessageSink messageSink) =>
            loggerSinkConfiguration.Sink(new XUnitTestSink(messageSink));
    }
}
