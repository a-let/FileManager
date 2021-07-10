using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

using Serilog;
using Serilog.Events;
using Serilog.Extensions.Hosting;
using Serilog.Parsing;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Web.Middlewares
{
    public class CustomRequestLoggingMiddleware : IMiddleware
    {
        private readonly DiagnosticContext _diagnosticContext;
        readonly MessageTemplate _messageTemplate;

        public CustomRequestLoggingMiddleware(DiagnosticContext diagnosticContext)
        {
            _diagnosticContext = diagnosticContext;
            _messageTemplate = new MessageTemplateParser().Parse("HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms");
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (next == null)
                throw new ArgumentNullException(nameof(next));

            var start = Stopwatch.GetTimestamp();
            var collector = _diagnosticContext.BeginCollection();

            try
            {
                await next(context);

                LogCompletion(context, collector, LogEventLevel.Information, context.Response.StatusCode, GetElapsedMilliseconds(start, Stopwatch.GetTimestamp()));
            }
            catch (Exception ex)
            {
                LogCompletion(context, collector, LogEventLevel.Error, context.Response.StatusCode, GetElapsedMilliseconds(start, Stopwatch.GetTimestamp()), ex);
            }
            finally
            {
                collector.Dispose();
            }
        }

        private void LogCompletion(HttpContext httpContext, DiagnosticContextCollector collector, LogEventLevel level, int statusCode, double elapsedMs, Exception ex = null)
        {
            var logger = Log.ForContext<CustomRequestLoggingMiddleware>();

            if (!logger.IsEnabled(level))
                return;

            if (!collector.TryComplete(out var properties))
                properties = new List<LogEventProperty>();

            properties = properties.Concat(new[]
            {
                new LogEventProperty("RequestMethod", new ScalarValue(httpContext.Request.Method)),
                new LogEventProperty("RequestPath", new ScalarValue(GetPath(httpContext))),
                new LogEventProperty("StatusCode", new ScalarValue(statusCode)),
                new LogEventProperty("Elapsed", new ScalarValue(elapsedMs))
            });

            var logEvent = new LogEvent(DateTimeOffset.Now, level, ex, _messageTemplate, properties);
            logger.Write(logEvent);
        }

        private static double GetElapsedMilliseconds(long start, long stop)
        {
            return (stop - start) * 1000 / (double)Stopwatch.Frequency;
        }

        private static string GetPath(HttpContext httpContext)
        {
            /*
                In some cases, like when running integration tests with WebApplicationFactory<T>
                the RawTarget returns an empty string instead of null, in that case we can't use
                ?? as fallback.
            */
            var requestPath = httpContext.Features.Get<IHttpRequestFeature>()?.RawTarget;
            if (string.IsNullOrEmpty(requestPath))
            {
                requestPath = httpContext.Request.Path.ToString();
            }

            return requestPath;
        }
    }
}
