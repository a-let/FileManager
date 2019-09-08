
using Logging;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Web.Middlewares
{
    public class CustomExceptionHandlerAsync
    {
        private readonly RequestDelegate _next;
        private readonly Type[] _exceptionTypes = new[] {typeof(ArgumentException), typeof(ArgumentNullException), typeof(NotImplementedException) };

        public CustomExceptionHandlerAsync(RequestDelegate next)
        {
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
            //response.ContentType = "application/json";
            response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;

            if (_exceptionTypes.Contains(ex.GetType()))
                await response.WriteAsync(ex.Message);
            else
                await response.WriteAsync("Error occured");

            var logger = context.RequestServices.GetRequiredService<ILogger>();
            await logger.LogErrorAsync(ex, ex.Message);
        }
    }
}