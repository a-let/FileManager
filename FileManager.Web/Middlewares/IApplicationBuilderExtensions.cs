using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

using SimpleInjector;

using System;
using System.Linq;

namespace FileManager.Web.Middlewares
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseCustomRequestLogging(this IApplicationBuilder app, Container container)
        {
            app.UseMiddleware<CustomRequestLoggingMiddleware>(container);

            return app;
        }

        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(new ExceptionHandlerOptions
            {
                ExceptionHandler = async (context) =>
                {
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    var problemDetailsFactory = context.RequestServices.GetRequiredService<ProblemDetailsFactory>();

                    var allowedExceptionTypes = new[]
                    {
                        typeof(ArgumentException),
                        typeof(ArgumentNullException),
                        typeof(NotImplementedException)
                    };

                    if (!allowedExceptionTypes.Contains(exceptionHandlerPathFeature.Error.GetType()))
                    {
                        context.Response.StatusCode = 500;
                        var problem = problemDetailsFactory.CreateProblemDetails(context, statusCode: 500, title: "Internal Server Error");
                        await context.Response.WriteAsJsonAsync(problem);
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                        var problem = problemDetailsFactory.CreateProblemDetails(context, statusCode: 400, title: "Bad Request", detail: exceptionHandlerPathFeature.Error.Message);
                        await context.Response.WriteAsJsonAsync(problem);
                    }
                }
            });

            return app;
        }

        public static IApplicationBuilder EnableSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FileManger V1");
                c.RoutePrefix = string.Empty;
            });

            return app;
        }
    }
}