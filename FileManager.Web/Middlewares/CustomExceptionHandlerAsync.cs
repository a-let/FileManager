using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Web.Middlewares
{
    public class CustomExceptionHandlerAsync
    {
        private readonly ILogger<CustomExceptionHandlerAsync> _logger;
        private readonly RequestDelegate _next;
        private readonly Type[] _allowedExceptionTypes = new[] 
        {
            typeof(ArgumentException),
            typeof(ArgumentNullException),
            typeof(NotImplementedException)
        };

        public CustomExceptionHandlerAsync(RequestDelegate next, ILogger<CustomExceptionHandlerAsync> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = StatusCodes.Status500InternalServerError;

            var remoteIp = context.Connection.RemoteIpAddress.ToString();
            _logger.LogError(ex, "IP: {RemoteIp} - {Message}", remoteIp, ex.Message);

            var responseMessage = !_allowedExceptionTypes.Contains(ex.GetType()) ?
                CreateMessage("Error occured") :
                CreateMessage(ex.Message);

            await response.WriteAsync(responseMessage);
        }

        private static string CreateMessage(string messageText) =>
            JsonConvert.SerializeObject(new { Message = messageText });
    }
}