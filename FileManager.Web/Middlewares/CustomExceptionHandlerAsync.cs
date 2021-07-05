using Logging;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Web.Middlewares
{
    public class CustomExceptionHandlerAsync
    {
        private readonly RequestDelegate _request;
        private readonly Type[] _allowedExceptionTypes = new[] 
        {
            typeof(ArgumentException),
            typeof(ArgumentNullException),
            typeof(NotImplementedException)
        };

        public CustomExceptionHandlerAsync(RequestDelegate request)
        {
            _request = request;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _request.Invoke(context);
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

            if (!_allowedExceptionTypes.Contains(ex.GetType()))
                await response.WriteAsync(CreateMessage("Error occured"));

            await response.WriteAsync(CreateMessage(ex.Message));

            var logger = context.RequestServices.GetRequiredService<ILogger>();
            await logger.LogErrorAsync(ex, ex.Message);
        }

        private string CreateMessage(string messageText) => JsonConvert.SerializeObject(new { Message = messageText });
    }
}